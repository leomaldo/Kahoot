#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
//-----------------------------------
/*#include <my_global.h>*/
/*#include <cstdio>*/
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int posicion_vector;
int contadorpartidas;

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
	if (!mysql_real_connect(conn,/* "shiva2.upc.es"*/"localhost", "root", "mysql",/*"MG5_Kahoot"*/"Kahoot", 0, NULL, 0)) {
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
	int id;
	char jugadores[512];
	int id_usuario;
	int preguntas_correctas;
} Partida;
typedef struct{
	Partida partida[512];
	int num;
}ListaPartidas;
	ListaPartidas listapartidas;
	
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
		//return NULL;
			return -1;
	}
	resultado = mysql_store_result(conn);
	
	row = mysql_fetch_row(resultado);
	
	if (row == NULL) {
		printf("No se han obtenido datos en la consulta\n");
		//return NULL;
			return -1;
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
int Invitar(char invitado[500], char nombre[25]) {
	//invitados; nombre: quien invita
	//Retorna numero de invitados --> Todo OK (+ notifica a los invitados (8/persona_que_le_ha_invitado*id_partida))
	//       -1 --> Alguno de los usuarios invitados se ha desconectado (+nombres de los desconectados en noDisponibles)
	
	int error = 0;
	
		int encontrado = 0;
		int i = 0;
		while ((i<miLista.num)&&(encontrado == 0)) {
			if (strcmp(miLista.conectados[i].nombre,invitado) == 0) {
				char invitacion[512];
				sprintf(invitacion, "7/%s", nombre);
				//Invitacion: 7/quien invita
				printf("Invitacion: %s\n",invitacion);
				write(miLista.conectados[i].socket, invitacion, strlen(invitacion));
				encontrado = 1;
			}
			else
				i = i + 1;
		}
		if (encontrado == 0){
			error = -1;
		}
		
	
	if(error == 0){
		
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
int AnadiraLista ( char *nombre[20], int socket) {
	//a\U00061925 nuevo conectado. Retorna 0 si ok y -1 si la lista esta llena
	
	if (miLista.num == 100)
	{
		return -1;
	}
	else{
		Conectado nuevoConectado;
		strcpy (nuevoConectado.nombre, *nombre);
		nuevoConectado.socket=socket;
		miLista.conectados[miLista.num - 1]=nuevoConectado;
		//printf("el nuevo conectado se ha guardado en esta posicion: %s\n", miLista.num);
		miLista.num=miLista.num+1;
		return 0;
	}
}

Partida crearPartida( char Jugadores[512])
{
	listapartidas.num=contadorpartidas+1;
     Partida partida;
	 partida.id=listapartidas.num-1;;
	 strcpy(partida.jugadores,Jugadores);
	 listapartidas.partida[listapartidas.num-1]=partida;
	 listapartidas.num=listapartidas.num+1;
	 return partida;
}

//Funcion que le pasas como parametro una variable donde guarda el numero de conectados que hay con sus respectivos nombres
void DameConectados(char conectados[300]) {
	// Pone en conectados los nombres de todos los conectados separados por "/"
	// 3/juan/maria/guillestrcpy(list, "\0");

	strcpy(conectados, "\0");
		int i;
		for(i=0; i< miLista.num; i++)
			sprintf(conectados, "%s%s&", conectados, miLista.conectados[i].nombre);
		conectados[strlen(conectados)-1]='\0';
	
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
void NotificarNuevaListaConectados(){
	
	char lista[512]={0};
	char notificacion[512];
	
	//pthread_mutex_lock(&mutex);
    DameConectados(lista);
	//pthread_mutex_unlock(&mutex);
	
	
		sprintf(notificacion,"-1/%s",lista);
		
	//Envia actualización a todos los sockets
	int j;
	for (j=0;j<miLista.num;j++){
		write(miLista.conectados[j].socket,notificacion,strlen(notificacion));
		printf("nombres lista conectados: %s\n", miLista.conectados[j].nombre);
	}
	
}
int DameSocketConectado(char nombre[25]){
	//Retorna -1 --> El usuario buscado no se ha encontrado conectado.
	//Retorna nÃºmero socket--> Se ha encontrado el resultado conectado.
	int i=0;
	int encontrado=0;
	while (i<miLista.num && !encontrado){
		if (strcmp(miLista.conectados[i].nombre,nombre)==0)
			encontrado=1;
		else
			i++;
	}
	if (encontrado==1)
	{
		return miLista.conectados[i].socket;
	}
	else 
		return -1;
}

void EnviarComenzarPartida(int numForm, char invitados[512], int Idpartida[100]) {
	char invitados_copia[strlen(invitados) + 1];
	strcpy(invitados_copia, invitados);
	char *nombre = strtok(invitados_copia, "&");// Obtener el primer nombre
	while (nombre != NULL) {
		// Obtener el socket del usuario
		int socket = DameSocketConectado(nombre);
		if (socket != NULL) {
			// Construir el mensaje
			char mensaje[100];
			
			sprintf(mensaje, "9/%d/%d/%s", numForm, Idpartida,invitados);
			
			write(socket, mensaje, strlen(mensaje));
			
			printf("Mensaje enviado a %s\n", nombre);
		} else {
			printf("No se pudo obtener el socket para %s\n", nombre);
		}
		// Obtener el siguiente nombre
		nombre = strtok(NULL, "&");
	}
}
void enviarchat(char mensaje[125], int id)
{
	int i=0;
	int encontrado=0;
	while(encontrado==0 && i<listapartidas.num)		
	{
		printf("%d",listapartidas.partida[i].id);
		printf("%d",id);
		if(listapartidas.partida[i].id==id)
		{
			encontrado=1;
		}
		else{
			i++;
		}
	}
	if(encontrado==1)
	{
		char jugadores_copia[strlen(listapartidas.partida[i].jugadores) + 1];
		strcpy(jugadores_copia, listapartidas.partida[i].jugadores);
		char *nombre = strtok(jugadores_copia, "&");
		while (nombre != NULL) {
			// Obtener el socket del usuario
			int socket = DameSocketConectado(nombre);
			if (socket != NULL) {
				// enviar mensaje
				write(socket, mensaje, strlen(mensaje));
				
				printf("Mensaje enviado a %s\n", nombre);
			} else {
				printf("No se pudo obtener el socket para %s\n", nombre);
			}
			// Obtener el siguiente nombre
			nombre = strtok(NULL, "&");
		}
	}
	else
	   printf("no encontrado");
}
//Funcion principal del programa donde determinamos que es lo que ha pedido el cliente
void *handleClientRequest (void *arg) {
	
	int posicion;
	int *s;
	s= (int *) arg;
	posicion= *s;
	printf("valor posicion que es lo que se le pasa a handlerequest: %d\n",posicion);
	miLista.num = posicion;
	int clientSocket = miLista.conectados[posicion - 1].socket;
	char request[512];
	char response[512];
	/*listapartidas.num=contadorpartidas;*/
	//saber si una persona se ha registrado o logueado
	printf("valor posicion -1:%d\n", posicion -1);
	printf("valor posicion_vector:%d\n", posicion_vector);
	while (1) {
		int ret = read(miLista.conectados[posicion -1].socket, request, sizeof(request));
		
		if (ret <= 0) {
			printf("Cliente desconectado\n");
			break;
		}
		
		request[ret] = '\0';
		printf("Recibo: %s ", request);
		char *p = strtok (request, "/");
		int code = atoi(p);
		int numForm;
		char nombre[512];
		
		struct ConexionBD conexion = {
			.servidor = "localhost", //"localhost","shiva2.upc.es"
				.usuario = "root",
				.contrasena = "mysql",
				.base_datos ="Kahoot"/* "MG5_Kahoot"*/
		};
		
		MYSQL *conn = conectarBD(&conexion);
		if (conn == NULL) {
			// Si hay un error en la conexión, salir del programa
			break;
		}	
		if (code == 1) {
			p = strtok (NULL, "/");
			numForm = atoi(p);
			int res2= PtsDeLaPartidaConMasPts(conn);
			sprintf(response, "1/%d/%d",numForm, res2);
			printf ("Respuesta: %s\n", response);
		}else if (code == 2) {
			p = strtok (NULL, "/");
			numForm = atoi(p);
			int res = JugadorMaxPtsTotales(conn, nombre);
			sprintf(response, "2/%d/%s",numForm, nombre);
			printf ("Respuesta: %s\n", response);
			
		} else if (code == 3) {
			p = strtok (NULL, "/");
			numForm = atoi(p);
			int res3 = encontrarPartidaMenosCorrectas(conn);
			sprintf(response, "3/%d/%d",numForm, res3);
			printf ("Respuesta: %s\n", response);
			
		}else if (code == 4){ //iniciar_Sesion
			char *usuario = strtok(NULL, "/");
			char *contrasena = strtok(NULL, "/");
			strcpy(nombre, usuario);
			
			// Verificar las credenciales en la base de datos
			if (verificarCredenciales(conn, nombre, contrasena)) {
				sprintf(response, "4/%s", nombre);
				pthread_mutex_lock(&mutex); 
				int respuesta = AnadiraLista( &usuario, clientSocket);
				
				NotificarNuevaListaConectados();
				pthread_mutex_unlock(&mutex);
			}
			else 
			{
				strcpy(response, "4/99");
			}
			write(miLista.conectados[posicion - 1].socket,response, strlen(response));
			
		}else if (code == 5) {// registrarse

			char *usuario = strtok(NULL, "/");
			strcpy(nombre,usuario);
			char *contrasena = strtok(NULL, "/");
			registro(conn, usuario, contrasena);
			sprintf(response, "5/%s", usuario);
						
			pthread_mutex_lock(&mutex); 
			int respuesta = AnadiraLista( &usuario, clientSocket);
			
			NotificarNuevaListaConectados();
			pthread_mutex_unlock(&mutex);
			
			write(miLista.conectados[posicion - 1].socket,response, strlen(response));
		}
		
		else if (code ==  6) {
			//Mensaje en peticion: 6/invitado1*invitado2*...
			//Return en respuesta: 7/0 (Todo OK) ; 7/invitado_no_disponible1/... (si hay invitados que se han desconectado)
			char *p = strtok(NULL, "/");
			//char p = strtok(NULL, "/");
			char invitado[100];
			strcpy(invitado, p);
			char *p2= strtok(NULL, "/");
			char nomnre[100];
			strcpy(nombre,p2);
			printf("Invitado: %s\n", invitado);
		    char respuesta[512];
				int res = Invitar(invitado, p2);
				printf("Resultado de invitar: %d\n",res);
				
				if (res == -1){
					strcpy(response,"6/-1");
				}
				else{
					strcpy(response,"6/0");					 
				}
		//significa que todo ha ido bien
				//Codigo 7 --> Respuesta a una invitacion de partida
		}
		else if (code ==  7) {
			//Mensaje en peticion: 7/respuesta(SI/NO)/id_partida
			//Mensaje en respuesta: -
		    char *p = strtok(NULL,"/");
			char respuesta[3];
			strcpy(respuesta,p);
			char *p2 = strtok(NULL,"/");
			char Invitador[100];
			strcpy(Invitador,p2);
			printf("Respuesta de invitación: %s\n",respuesta);
			printf("Invitador: %s\n", Invitador);
			printf("Después de invitador envío: %s\n", response);
		int socketInvitador = DameSocketConectado(Invitador);
		char RespuestaInv[100];
			if (strcmp(respuesta,"1")==0){
				
				sprintf(RespuestaInv, "8/1/%s",nombre);
				write(socketInvitador, RespuestaInv, strlen(RespuestaInv));
			}
			else{
				sprintf(RespuestaInv, "8/-1/%s", nombre);
			write(socketInvitador, RespuestaInv, strlen(RespuestaInv));
			}
		   //mandar mensaje al que solicita de que ha aceptado o no
		}else if(code==8)
		{
			char *p = strtok(NULL, "/");
			numForm = atoi(p);
			p = strtok (NULL, "/");
			char invitados[512];
			strcpy(invitados,p);		
			Partida partida=crearPartida(invitados);
			EnviarComenzarPartida(numForm, partida.jugadores ,partida.id);
			contadorpartidas++;
		}
		else if (code == 10)
		{
			char *p1 = strtok(NULL, "/");
			char mensaje[512];
			strcpy (mensaje,p1);
			char *u = strtok(NULL, "/");
			char usuario[512];
			strcpy(usuario,u);
			char *i = strtok(NULL, "/");
			char id[512];
			strcpy(id,i);
			char respuesta[512];
			char respuestachat[512];
			sprintf(respuesta, "10/%s/%s/%s", mensaje, usuario, id);
			strcpy(response, respuesta);
			sprintf(respuestachat, "%s/%s", usuario, mensaje);
			
			enviarchat(respuesta,atoi(id));
		}
		else if (code == 0){
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
			
			printf("numero de milista.num es: %d \n", miLista.num);
			
			pthread_mutex_lock( &mutex);
			NotificarNuevaListaConectados();
			pthread_mutex_unlock( &mutex);
			sprintf(response, "0/%s", nombre);
			
			
		}
		if (code !=0 && code != 5 && code != 4 && code != 7 && code != 10 && code !=8)
		{
			printf ("Respuesta: %s\n", response);
			//Enviamos respuesta
			write(miLista.conectados[posicion - 1].socket,response, strlen(response));
		}
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
		.servidor ="localhost", //"localhost","shiva2.upc.es"
			.usuario = "root",
			.contrasena = "mysql",
			.base_datos ="Kahoot"/* "MG5_Kahoot"*/
	};
	
	// Conectar a la base de datos utilizando la estructura de conexión
	MYSQL *conn = conectarBD(&conexion);
	if (conn == NULL) {
		// Si hay un error en la conexión, salir del programa
		return 1;
	}
	
	printf("Conexión a la base de datos establecida correctamente.\n");		
	int serverSocket;
	int puerto = 50023; //50023;
	struct sockaddr_in serverAddr, clientAddr;
	socklen_t addrLen = sizeof(struct sockaddr_in);
	serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	serverAddr.sin_port = htons(puerto); //9052//50020
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
