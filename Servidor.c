#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
//#include <my_global.h>*/
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int posicion_vector;
 
struct ConexionBD {
	const char *servidor;
	const char *usuario;
	const char *contrasena;
	const char *base_datos;
};

// Función para conectar a la base de datos utilizando la estructura de conexión
MYSQL *conectarBD(struct ConexionBD *conexion) {
	MYSQL *conn = mysql_init(NULL); // Inicializar el objeto de conexión
	
	// Establecer la conexión a la base de datos
	if (!mysql_real_connect(conn, /*"shiva2.upc.es"*/"localhost", "root", "mysql",/*"MG5_Kahoot"*/"Kahoot", 0, NULL, 0)) {
		fprintf(stderr, "Error al conectar a la base de datos: %s\n", mysql_error(conn));
		mysql_close(conn);
		return NULL; // Devolver NULL si hay un error
	}
	
	return conn; // Devolver el objeto de conexión si la conexión fue exitosa
}

typedef struct { 
	char nombre [20];
	int socket;
} Conectado;
//clases para ver los conectados
typedef struct {
	Conectado conectados [100];
	int num;
} ListaConectados;
//declaracion listaDeConectados como variable global
ListaConectados miLista;

typedef struct {
	int id_partida;
	int id_usuario;
	int preguntas_correctas;
} Partida;

typedef struct {
	int id_usuario;
	char nombre_usuario[100];
} Jugador;

//funcion que devuelve el jugador con mas puntos totales
int JugadorMaxPtsTotales(MYSQL *conn, char nombre [20]) {
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char jugador[100];
	
	err = mysql_query(conn, "SELECT nombre_usuario, numero_puntos_totales FROM jugadores ORDER BY numero_puntos_totales DESC");
	
	if (err != 0) {
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		return NULL;
	}
	resultado = mysql_store_result(conn);
	
	row = mysql_fetch_row(resultado);
	
	if (row == NULL) {
		printf("No se han obtenido datos en la consulta\n");
		return NULL;
	}
	
	strcpy(nombre, row[0]);
	
	mysql_free_result(resultado);
	//printf("%s",jugador);
// = strdup(jugador); // Duplicar la cadena para evitar problemas de memoria
	return 0;
}

//funcion que devuelve los puntos de la partida que mas puntos tienen
int PtsDeLaPartidaConMasPts(MYSQL *conn) {
	int puntosMasPuntos = -1; // Valor inicial para indicar que no se encontraron partidas
	
	char query[200];
	sprintf(query, "SELECT puntos FROM partida ORDER BY puntos DESC LIMIT 1");
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al ejecutar la consulta: %s\n", mysql_error(conn));
		return -1;
	}
	MYSQL_RES *result = mysql_store_result(conn);
	
	if (result == NULL) {
		fprintf(stderr, "Error al obtener el resultado: %s\n", mysql_error(conn));
		return -1;
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	
	if (row != NULL) {
		puntosMasPuntos = atoi(row[0]);
	} else {
		printf("No se encontraron partidas.\n");
	}
	mysql_free_result(result);
	//printf("%d",puntosMasPuntos);
	
	return puntosMasPuntos;
}

//funcion que retorna la partida con menos preguntas correctas
int encontrarPartidaMenosCorrectas(MYSQL *conn) {
	char query[200];
	sprintf(query, "SELECT ID_partida FROM partida ORDER BY numero_preguntas_correctas ASC LIMIT 1");
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al ejecutar la consulta: %s\n", mysql_error(conn));
		return -1;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	
	if (result == NULL) {
		fprintf(stderr, "Error al obtener el resultado: %s\n", mysql_error(conn));
		return -1;
	}
	MYSQL_ROW row = mysql_fetch_row(result);
	
	if (row != NULL) {
		int partidaId = atoi(row[0]);
		mysql_free_result(result);
		return partidaId;
	} else {
		printf("No se encontraron partidas.\n");
		mysql_free_result(result);
		return -1;
	}
}
int Invitar(char invitados[500], char nombre[25], char noDisponibles[500]) {
	//invitados; nombre: quien invita
	//Retorna numero de invitados --> Todo OK (+ notifica a los invitados (8/persona_que_le_ha_invitado*id_partida))
	//       -1 --> Alguno de los usuarios invitados se ha desconectado (+nombres de los desconectados en noDisponibles)
	
	strcpy(noDisponibles,"\0");
	int error = 0;
	char *p = strtok(invitados,"/");
	int numInvitados = 0;
	while (p != NULL) {
		int encontrado = 0;
		int i = 0;
		while ((i<miLista.num)&&(encontrado == 0)) {
			if (strcmp(miLista.conectados[i].nombre,p) == 0) {
				char invitacion[512];
				sprintf(invitacion, "8/%s*%d*", nombre);
				//Invitacion: 8/quien invita*id_partida
				printf("Invitacion: %s\n",invitacion);
				write(miLista.conectados[i].socket, invitacion, strlen(invitacion));
				encontrado = 1;
				numInvitados = numInvitados +1;
			}
			else
				i = i + 1;
		}
		if (encontrado == 0){
			error = -1;
			sprintf(noDisponibles,"%s%s/",noDisponibles,p);
			noDisponibles[strlen(noDisponibles)-1] = '\0';
		}
		p = strtok(NULL, "/");		
	}
	if(error == 0){
		error = numInvitados;
		pthread_mutex_lock(&mutex);
		
		pthread_mutex_unlock(&mutex);
	}
	
	return error;
}
//funcion que registra en la base de datos a un usuario 
void registro(MYSQL *conn, const char *usuario, const char *contrasena) {
	char query[100];
	sprintf(query, "INSERT INTO usuarios (usuario, contrasena) VALUES ('%s', '%s')", usuario, contrasena);
	
/*	if (mysql_query(conn, query)) {*/
/*		fprintf(stderr, "Error al insertar datos: %s\n", mysql_error(conn));*/
/*	} else {*/
/*		printf("Registro exitoso para usuario: %s\n", usuario);*/
/*	}*/
	// metemos este usuario en la lista de usuarios activos
}

//funcion que verifica las credenciales de un usuario en la base de datos
int verificarCredenciales(MYSQL *conn, const char *usuario, const char *contrasena) {
	char query[100];
	sprintf(query, "SELECT * FROM usuarios WHERE usuario='%s' AND contrasena='%s'", usuario, contrasena);
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al verificar credenciales: %s\n", mysql_error(conn));
		return 0;
	}
	MYSQL_RES *result = mysql_store_result(conn);
	int num_rows = mysql_num_rows(result);
	
	mysql_free_result(result);
	
	return num_rows > 0;
}

//funcion que pone los conectados en el vector de conectados de la lista de conectados
int Pon ( char *nombre[20], int socket) {
	//a\U00061925 nuevo conectado. Retorna 0 si ok y -1 si la lista esta llena
	
	if (miLista.num == 100)
	{
		return -1;
	}
	else{
		strcpy (miLista.conectados[miLista.num -1].nombre, *nombre);
		miLista.conectados[miLista.num -1].socket = socket;
		return 0;
		printf("hola he entrado \n");
	}
}

//Funcion que le pasas como parametro una variable donde guarda el numero de conectados que hay con sus respectivos nombres
void DameConectados ( char conectados[300]) {
	//pone en conectados los nombres de todos los conectados separados por "/"
	// 3/juan/maria/guille	 
	sprintf (conectados, "%d", miLista.num);
	int i;
	for (i=0; i< miLista.num; i++)
	{
		sprintf (conectados, "%s/%s",conectados, miLista.conectados[i].nombre);
	}
	printf("%s\n", conectados);	
}

//Funcion que elimina a un usuario cuando este se desconecta
int Eliminar ( int position) {
	
	int i;
	for (i = position; i< miLista.num-1;i++)
	{
		strcpy(miLista.conectados[i].nombre , miLista.conectados[i+1].nombre);
		miLista.conectados[i].socket = miLista.conectados[i+1].socket;
	}
	miLista.num--;
	posicion_vector--;
	return 0;
}

//Funcion principal del programa donde determinamos que es lo que ha pedido el cliente
void *handleClientRequest (void *arg) {
	
	int posicion;
	int *s;
	s= (int *) arg;
	posicion= *s;
	miLista.num = posicion;
	int clientSocket = miLista.conectados[posicion - 1].socket;
	char request[512];
	char response[512];
	//saber si una persona se ha registrado o logueado
	
	while (1) {
		int ret = read(miLista.conectados[posicion -1].socket, request, sizeof(request));
		
		if (ret <= 0) {
			printf("Cliente desconectado\n");
			break;
		}
		
		request[ret] = '\0';
		printf("Recibo: %s ", request);
		int code = atoi(strtok(request, "/"));
		char usuario[512];
		
		struct ConexionBD conexion = {
			.servidor = /*"shiva2.upc.es"*/"localhost",
				.usuario = "root",
				.contrasena = "mysql",
				.base_datos =/* "MG5_Kahoot"*/"Kahoot"
		};
		
		MYSQL *conn = conectarBD(&conexion);
		if (conn == NULL) {
			// Si hay un error en la conexión, salir del programa
			break;
		}	
		if (code == 2) {
			char nombre [20];
			int res = JugadorMaxPtsTotales(conn, nombre);
			sprintf(response, "2/%s", nombre);
		} else if (code == 1) {
			int res2= PtsDeLaPartidaConMasPts(conn);
			sprintf(response, "1/%d", res2);
		} else if (code == 3) {
			int res3 = encontrarPartidaMenosCorrectas(conn);
			sprintf(response, "3/%d", res3);
		}else if (code == 5) {
			//variable = 0;
			char *usuario = strtok(NULL, "/");
			char *contrasena = strtok(NULL, "/");
			registro(conn, usuario, contrasena);
			sprintf(response, "5/Registro exitoso para usuario: %s", usuario);
			int respuesta = Pon( &usuario, clientSocket);
			if (respuesta ==0)
			{
				char conectados_string [300];
				DameConectados( conectados_string);			
				//notificar a todos los clientes conectados
				char notificacion [20];
				sprintf (notificacion, "0/%s" , conectados_string);
				int j=0;
				for ( j=0; j< miLista.num; j++)
					write(miLista.conectados[j].socket, notificacion, strlen(notificacion));
			}
			//printf("el valor que retorna la funcion Pon es: %d\n", respuesta);	
		}
		else if (code ==  6) {
			//Mensaje en peticion: 6/invitado1*invitado2*...
			//Return en respuesta: 7/0 (Todo OK) ; 7/invitado_no_disponible1/... (si hay invitados que se han desconectado)
			
			char p = strtok(NULL, "/");
			char invitados[500];
			printf("Invitados: %s\n", invitados);
			char noDisponibles[500];
			strcpy(invitados, p);
		    char respuesta[512];
				int res = Invitar(invitados, usuario, noDisponibles);
				printf("Resultado de invitar: %d\n",res);
				
				if (res == -1){
					sprintf(respuesta,"7/%s",noDisponibles);
				}
				else{
					strcpy(respuesta,"7/0");						 
				}
		//significa que todo ha ido bien
				//Codigo 7 --> Respuesta a una invitacion de partida
		}
		else if (code ==  7) {
			//Mensaje en peticion: 7/respuesta(SI/NO)/id_partida
			//Mensaje en respuesta: -
			
			char p = strtok(NULL,"/");
			char respuesta1[0];
			strcpy(respuesta1,p);
			printf("%s\n",respuesta1);
			p = strtok(NULL,"/");
			
		   //mandar mensaje al que solicita de que ha aceptado o no
			
		}
		else if (code == 4){
			char *usuario = strtok(NULL, "/");
			char *contrasena = strtok(NULL, "/");
			// Verificar las credenciales en la base de datos
			if (verificarCredenciales(conn, usuario, contrasena)) {
				sprintf(response, "4/Inicio de sesión exitoso para usuario: %s", usuario);
			}
			else {
				sprintf(response, "4/Credenciales incorrectas");
			}
			int respuesta = Pon( &usuario, clientSocket);
		    if (respuesta ==0)
			{
			char conectados_string [300];
			DameConectados( conectados_string);			
			//notificar a todos los clientes conectados
			char notificacion [20];
			sprintf (notificacion, "0/%s" , conectados_string);
			int j=0;
			for ( j=0; j<miLista.num ; j++)
				write(miLista.conectados[j].socket, notificacion, strlen(notificacion));
		    }
			//printf("el valor que retorna la funcion Pon es: %d\n", respuesta);
		
/*		else if (code==6){*/
/*			char conectados_string [300];*/
/*			pthread_mutex_lock( &mutex);*/
/*			DameConectados( conectados_string);*/
/*			pthread_mutex_unlock( &mutex);*/
			
		//}
		}
		if (code !=0)
		{
			printf ("Respuesta: %s\n", response);
			//Enviamos respuesta
			write (miLista.conectados[posicion - 1].socket,response, strlen(response));
			//write (miLista.conectados[posicion_vector].socket,response, strlen(response
		}
		else if (code == 0){
			printf("mensaje de desconexion victor\n");
			int j = 0;
			printf("socket para eliminar: %d \n", clientSocket);
			int encontrado=0;
			while ((j< miLista.num) && (encontrado == 0))
			{
				if (clientSocket == miLista.conectados[j].socket)
				{
					encontrado = 1;
				}
				else 
				{
					j++;
				}
			}
			pthread_mutex_lock( &mutex);
			int resp_eli = Eliminar ( j);
			pthread_mutex_unlock( &mutex);
			
			char conectados_string [300];
			DameConectados( conectados_string);
			//notificar a todos los clientes conectados
			char notificacion [20];
			sprintf (notificacion, "0/%s" , conectados_string);			
			j=0;
			for ( j=0; j<miLista.num ; j++)
				write(miLista.conectados[j].socket, notificacion, strlen(notificacion));
        }
/*		pthread_mutex_lock( &mutex);*/
		//printf ("Respuesta: %s\n", response);
/*		write(miLista.conectados[posicion_vector].socket, response, strlen(response));*/
/*		pthread_mutex_unlock( &mutex);		*/
	}
	close(miLista.conectados[posicion_vector].socket); 
}

struct thread_info {    /* Used as argument to thread_start() */
	pthread_t thread_id;        /* ID returned by pthread_create() */
	int       thread_num;       /* Application-defined thread # */
	char     *argv_string;      /* From command-line argument */
};

int main() {
	
	//estructura base de datos
	struct ConexionBD conexion = {
		.servidor =/* "shiva2.upc.es"*/"localhost",
			.usuario = "root",
			.contrasena = "mysql",
			.base_datos ="Kahoot"
	};
	
	// Conectar a la base de datos utilizando la estructura de conexión
	MYSQL *conn = conectarBD(&conexion);
	if (conn == NULL) {
		// Si hay un error en la conexión, salir del programa
		return 1;
	}
	
	printf("Conexión a la base de datos establecida correctamente.\n");		
	int serverSocket;
	int puerto = 50020;
	struct sockaddr_in serverAddr, clientAddr;
	socklen_t addrLen = sizeof(struct sockaddr_in);
	serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	serverAddr.sin_port = htons(puerto); //9052
	bind(serverSocket, (struct sockaddr *) &serverAddr, sizeof(serverAddr));
	listen(serverSocket, 3);
	
	pthread_t thread;
	posicion_vector = 0;
	
	for (;;) {
		printf("Esperando conexiones1...\n");
		int clientSocket = accept(serverSocket, (struct sockaddr *) &clientAddr, &addrLen);
		printf("Cliente conectado\n");
		printf("%d \n", clientSocket);
		miLista.conectados[posicion_vector].socket = clientSocket;
		printf("el socket en el for del main es %d \n", miLista.conectados[posicion_vector].socket);
		pthread_create (&thread, NULL,  &handleClientRequest,&posicion_vector);
		printf("%d \n", posicion_vector);
		posicion_vector++;
		
	}
	close(serverSocket);
	
	mysql_close(conn);
	
	return 0;
}
