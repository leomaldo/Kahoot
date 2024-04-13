#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
//prueba
//15:36 11_4

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

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
	if (!mysql_real_connect(conn, conexion->servidor, conexion->usuario, conexion->contrasena, conexion->base_datos, 0, NULL, 0)) {
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
char* JugadorMaxPtsTotales(MYSQL *conn) {
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
	
	strcpy(jugador, row[0]);
	
	mysql_free_result(resultado);
    printf("%s",jugador);
	char* jugadorNombre = strdup(jugador); // Duplicar la cadena para evitar problemas de memoria
	return jugadorNombre;;
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
	printf("%d",puntosMasPuntos);
	
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

//funcion que registra en la base de datos a un usuario 
void registro(MYSQL *conn, const char *usuario, const char *contrasena) {
	char query[100];
	sprintf(query, "INSERT INTO usuarios (usuario, contrasena) VALUES ('%s', '%s')", usuario, contrasena);
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al insertar datos: %s\n", mysql_error(conn));
	} else {
		printf("Registro exitoso para usuario: %s\n", usuario);
	}
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
// int Pon (ListaConectados *lista, char *nombre[20], int socket) { ------------- //int Pon (ListaConectados *lista, char nombre[20], int socket) {
int Pon ( char *nombre[20], int socket) {
	//a񡤥 nuevo conectado. Retoirna 0 si ok y -1 si la lista esta llena
	
	if (miLista.num == 100)
	{
		return -1;
	}
	else{
		strcpy (miLista.conectados[miLista.num -1].nombre, *nombre);
		miLista.conectados[miLista.num -1].socket = socket;
		//miLista.num++;
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

/*int DamePosicion (ListaConectados *lista, char nombre [20]) {*/
	//devuelve la posicion o -1 si no esta en la lista
/*	int i= 0;*/
/*	int encontrado = 0;*/
/*	while ((i < lista->num) && (encontrado == 0))*/
/*	{*/
/*		if (strcmp(lista->conectados[i].nombre, nombre) == 0)*/
/*		{*/
/*			encontrado = 1;*/
/*		}*/
/*		else */
/*		{*/
/*			i++;	*/
/*		}*/
/*	}*/
/*	if (encontrado == 1)*/
/*	{*/
/*		return i;*/
/*	}*/
/*	else*/
/*	{*/
/*		return -1;*/
/*	}*/
/*}*/
//elimina a un conectado de la lista


//Funcion que elimina a un usuario cuando este se desconecta
//int Eliminar ( char *nombre[20], int position) {
int Eliminar ( int position) {
//int pos = DamePosicion (nombre);
/*	if (pos==-1)*/
/*	{*/
/*		return -1;*/
/*	}*/
	//else 
	//{
		int i;
		for (i = position; i< miLista.num-1;i++)
		{
			strcpy(miLista.conectados[i].nombre , miLista.conectados[i+1].nombre);
			miLista.conectados[i].socket = miLista.conectados[i+1].socket;
		}
		miLista.num--;
		return 0;
	//}
	
}

//Funcion principal del programa donde determinamos que es lo que ha pedido el cliente
//void handleClientRequest(int clientSocket, MYSQL *conn) {
void *handleClientRequest (void *arg) {
	
	//10/4 int *clientSocket = (int *)arg;
	int posicion;
	int *s;
	s= (int *) arg;
	posicion= *s;
	miLista.num = posicion;
	//miLista.conectados[posicion].socket;
	int clientSocket = miLista.conectados[posicion - 1].socket;
	//int *clientSocket = &miLista.conectados[posicion].socket;
/*	printf("la variable posicion: %d \n", posicion);*/
/*	printf("hola, el socket es: %d\n",miLista.conectados[posicion - 1].socket);*/
/*	printf("la variable *clientsocket vale: %d \n", clientSocket);*/
	char request[512];
	char response[512];
	//saber si una persona se ha registrado o logueado
	//int variable;
	
	while (1) {
		//int ret = read(*clientSocket, request, sizeof(request));
		int ret = read(miLista.conectados[posicion -1].socket, request, sizeof(request));
		
		if (ret <= 0) {
			printf("Cliente desconectado\n");
			break;
		}
		
		request[ret] = '\0';
	int code = atoi(strtok(request, "/"));

	struct ConexionBD conexion = {
		.servidor = "localhost",
			.usuario = "root",
			.contrasena = "mysql",
			.base_datos = "Kahoot"
	};

	MYSQL *conn = conectarBD(&conexion);
	if (conn == NULL) {
		// Si hay un error en la conexión, salir del programa
		break;
	}	
	if (code == 2) {
		char* res = JugadorMaxPtsTotales(conn);
		sprintf(response, "%s", res);
	} else if (code == 1) {
		int res2= PtsDeLaPartidaConMasPts(conn);
		sprintf(response, "%d", res2);
	} else if (code == 3) {
		int res3 = encontrarPartidaMenosCorrectas(conn);
		sprintf(response, "%d", res3);
	}else if (code == 5) {
		//variable = 0;
		char *usuario = strtok(NULL, "/");
		
		
		
		char *contrasena = strtok(NULL, "/");
		
		registro(conn, usuario, contrasena);
		
		sprintf(response, "Registro exitoso para usuario: %s", usuario);
		
		int respuesta = Pon( &usuario, clientSocket);
		printf("el valor que retorna la funcion Pon es: %d\n", respuesta);
		
	}else if (code == 4) {
		//variable = 1;
		
		char  *usuario = strtok(NULL, "/");
		
		char *contrasena = strtok(NULL, "/");
		
		// Verificar las credenciales en la base de datos
		if (verificarCredenciales(conn, usuario, contrasena)) {
			sprintf(response, "Inicio de sesión exitoso para usuario: %s", usuario);
		}else {
			sprintf(response, "Credenciales incorrectas");
		}
		//int respuesta = Pon( &miLista, &usuario, miLista.conectados[posicion -1].socket);
		int respuesta = Pon( &usuario, clientSocket);
		printf("el valor que retorna la funcion Pon es: %d\n", respuesta);
		
	}else if (code==6){
		char conectados_string [300];
		pthread_mutex_lock( &mutex);	
		DameConectados( conectados_string);
		pthread_mutex_unlock( &mutex);
		sprintf(response, "%s", conectados_string);
		//sprintf(response, conectados);
	}else if (code == 0){
		printf("mensaje de desconexion victor\n");
		int j = 0;
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
	}
	
	//write(*clientSocket, response, strlen(response));
	pthread_mutex_lock( &mutex);
	write(miLista.conectados[posicion - 1].socket, response, strlen(response));
	pthread_mutex_unlock( &mutex);
	}
}



struct thread_info {    /* Used as argument to thread_start() */
	pthread_t thread_id;        /* ID returned by pthread_create() */
	int       thread_num;       /* Application-defined thread # */
	char     *argv_string;      /* From command-line argument */
};

int main() {
	
	//structura base de datos
	struct ConexionBD conexion = {
		.servidor = "localhost",
			.usuario = "root",
			.contrasena = "mysql",
			.base_datos = "Kahoot"
	};
	
	// Conectar a la base de datos utilizando la estructura de conexión
	MYSQL *conn = conectarBD(&conexion);
	if (conn == NULL) {
		// Si hay un error en la conexión, salir del programa
		return 1;
	}
	
	printf("Conexión a la base de datos establecida correctamente.\n");		
	
	int serverSocket;
	//int clientSocket;
	struct sockaddr_in serverAddr, clientAddr;
	socklen_t addrLen = sizeof(struct sockaddr_in);
	
	serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	
	memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	serverAddr.sin_port = htons(9052); //9052
	bind(serverSocket, (struct sockaddr *) &serverAddr, sizeof(serverAddr));
	listen(serverSocket, 3);
	
	
	int i;
	//int sockets[100];
	pthread_t thread;
	i=0;
	
	
	
	
	for (;;) {
		printf("Esperando conexiones1...\n");
		
		int clientSocket = accept(serverSocket, (struct sockaddr *) &clientAddr, &addrLen);
		//int clientSocket = read(serverSocket, (struct sockaddr *) &clientAddr, &addrLen);
		printf("Cliente conectado\n");
		printf("%d \n", clientSocket);
		//sockets[i]= clientSocket;
		miLista.conectados[i].socket = clientSocket;
			printf("el socket en el for del main es %d \n", miLista.conectados[i].socket);
		
	//		handleClientRequest(clientSocket, conn);
	//10/4	pthread_create (&thread, NULL,  &handleClientRequest,(void *)&sockets[i]);
		//10/4_15:53 pthread_create (&thread, NULL,  &handleClientRequest,(void *)&i);
		pthread_create (&thread, NULL,  &handleClientRequest,&i);
		printf("%d \n", i);
	//	close(clientSocket);
		i++;
		
	}
	close(serverSocket);
	
	mysql_close(conn);
	
	return 0;
}
