#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
#include <time.h>
//-----------------------------------
/*#include <my_global.h>*/
/*#include <cstdio>*/
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

char finaliza_partida_string [200];

int posicion_vector;
int contadorpartidas=0;

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
	if (!mysql_real_connect(conn,"shiva2.upc.es", "root", "mysql","MG5_Kahoot", 0, NULL, 0)) {
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
	int contRespuestas;
	int contTerminar;
	char host[512];
	char fecha[100];
	char puntos[1500];
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
/*	char query[100];*/
/*	sprintf(query, "INSERT INTO usuarios (usuario, contrasena) VALUES ('%s', '%s')", usuario, contrasena);*/
	MYSQL_STMT *stmt;
	MYSQL_BIND bind[2];
	
	// Consulta SQL con placeholders
	const char *query = "INSERT INTO usuarios (usuario, contrasena) VALUES (?, ?)";
	
	// Inicializar la declaraciÃ³n
	stmt = mysql_stmt_init(conn);
	if (!stmt) {
		fprintf(stderr, "mysql_stmt_init() failed\n");
		return;
	}
	// Preparar la consulta
	if (mysql_stmt_prepare(stmt, query, strlen(query)) != 0) {
		fprintf(stderr, "mysql_stmt_prepare() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	
	// Limpiar el array bind
	memset(bind, 0, sizeof(bind));
	// Vincular el primer parÃ¡metro (usuario)
	bind[0].buffer_type = MYSQL_TYPE_STRING;
	bind[0].buffer = (char *)usuario;
	bind[0].buffer_length = strlen(usuario);
	
	// Vincular el segundo parÃ¡metro (contrasena)
	bind[1].buffer_type = MYSQL_TYPE_STRING;
	bind[1].buffer = (char *)contrasena;
	bind[1].buffer_length = strlen(contrasena);
	// Vincular los parÃ¡metros con la declaraciÃ³n
	if (mysql_stmt_bind_param(stmt, bind) != 0) {
		fprintf(stderr, "mysql_stmt_bind_param() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	
	// Ejecutar la declaraciÃ³n
	if (mysql_stmt_execute(stmt) != 0) {
		fprintf(stderr, "mysql_stmt_execute() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	// Cerrar la declaraciÃ³n
	mysql_stmt_close(stmt);
	printf("Usuario registrado con Ã©xito.\n");
}
void Baja(MYSQL *conn, const char *usuario, const char *contrasena){
/*	char query[100];*/
/*	sprintf (query, "DELETE * FROM usuarios WHERE usuario='%s' AND contrasena='%s'", usuario, contrasena);*/
	
/*	sprintf (query, "UPDATE usuarios SET nombre = 'Baja' WHERE nombre ='%s'", usuario);*/
	MYSQL_STMT *stmt;
	MYSQL_BIND bind[2];
	
	const char *query = "DELETE FROM usuarios WHERE usuario = ? AND contrasena = ?";
	
	stmt = mysql_stmt_init(conn);
	if (!stmt) {
		fprintf(stderr, "mysql_stmt_init() failed\n");
		return;
	}
	
	if (mysql_stmt_prepare(stmt, query, strlen(query)) != 0) {
		fprintf(stderr, "mysql_stmt_prepare() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	memset(bind, 0, sizeof(bind));
	
	bind[0].buffer_type = MYSQL_TYPE_STRING;
	bind[0].buffer = (char *)usuario;
	bind[0].buffer_length = strlen(usuario);
	
	bind[1].buffer_type = MYSQL_TYPE_STRING;
	bind[1].buffer = (char *)contrasena;
	bind[1].buffer_length = strlen(contrasena);
	if (mysql_stmt_bind_param(stmt, bind) != 0) {
		fprintf(stderr, "mysql_stmt_bind_param() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	
	if (mysql_stmt_execute(stmt) != 0) {
		fprintf(stderr, "mysql_stmt_execute() failed: %s\n", mysql_stmt_error(stmt));
		mysql_stmt_close(stmt);
		return;
	}
	mysql_stmt_close(stmt);
	printf("Usuario eliminado con Ã©xito.\n");
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

Partida crearPartida( char Jugadores[512], int iden, char host[512])
{
     Partida partida;
	 partida.id=iden;
	 partida.contRespuestas=0;
	 partida.contTerminar=0;
	 strcpy(partida.host,host);
	 strcpy(partida.jugadores,Jugadores);
	 listapartidas.partida[listapartidas.num]=partida;
     listapartidas.num++;	
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

void EnviarComenzarPartida(int numForm, char invitados[10000], int Idpartida,char host[512]) {
	char invitados_copia[strlen(invitados) + 1];
	strcpy(invitados_copia, invitados);
	char *nombre = strtok(invitados_copia, "&");// Obtener el primer nombre
	while (nombre != NULL) {
		// Obtener el socket del usuario
		int socket = DameSocketConectado(nombre);
		if (socket != -1) {
			// Construir el mensaje
			char mensaje[100];
			
			sprintf(mensaje, "9/%d/%d/%s/%s", numForm, Idpartida,invitados,host);
			
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
int pregunta_bd (MYSQL *conn, int id_partida)
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char query[100];
	//char resultado[100];
	char respuesta[200];
	// Inicializar el generador de nÃºmeros aleatorios
	srand(time(0));
	
	// Generar un nÃºmero aleatorio entre 0 y 11
	int randomNumber;
	do {
		randomNumber = rand() % 5;
	} while (randomNumber == 0);
	int numero = 5; // por si el random no va
	sprintf(query, "SELECT pregunta,respuesta_correcta,respuesta_incorrecta_1,respuesta_incorrecta_2,respuesta_incorrecta_3 FROM preguntas WHERE ID_pregunta = %d", /*numero*/ randomNumber); //iria randomNumber pero nose pk no va
	
	 err=mysql_query (conn, query);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
			sprintf(respuesta, "11/%d/%s/%s/%s/%s/%s",id_partida, row[0],row[1],row[2],row[3],row[4]);
			
	}
	
	printf("%s\n", respuesta);
	
	int i=0;
	int encontrado=0;
	while(encontrado==0 && i<listapartidas.num)		
	{
		printf("%d",listapartidas.partida[i].id);
		printf("%d",id_partida);
		if(listapartidas.partida[i].id==id_partida)
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
				write(socket, respuesta, strlen(respuesta));
				
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
void enviarpuntos(char mensaje[125], int id)
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
int personas_partida(id_partida)
{
	int cont= 0;
		char jugadores_copia[strlen(listapartidas.partida[id_partida].jugadores) + 1];
	strcpy(jugadores_copia, listapartidas.partida[id_partida].jugadores);
	char *nombre = strtok(jugadores_copia, "&");
	while (nombre != NULL) {
		cont++;
		nombre = strtok(NULL, "&");
	}
	return cont;
};

// code 15
int lista_jugadores_con_los_que_ha_jugado(MYSQL *conn, char usuario_peticion_4[20])
{
	
	int i = 1;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char query[100];
	
	char respuesta[300];
	int encontrado = 0;
	char query2[100];
	int Id_maximo = 0;
	char jugadores[100];
	int entra = 0;
		
		
	strcpy(query, "SELECT max(Id_partida) FROM partida_jugada");
	
		err=mysql_query (conn, query);
		if (err!=0)
		{
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return -1;
		}
	
	
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
		{
			 Id_maximo = atoi (row[0]);
		}
		
		printf ("Id_maximo =%d \n",Id_maximo );
	
		while (i <= Id_maximo)
		{
			sprintf(query2, "SELECT nombre_jugadores FROM partida_jugada WHERE Id_partida = %d ", i);
			
			err=mysql_query (conn, query2);
			if (err!=0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				return -1;
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				
				//char *nombre = strtok (row[0], "&") ;
				
			}
			
			
			printf ("HAGO LA QUERY\n" );
/*			resultado = mysql_store_result (conn);*/
/*			row = mysql_fetch_row (resultado);*/
			
			printf ("VALOR DEL SELECT=%s\n",row[0] );
			strcpy(jugadores, row[0]);
			
			char *nombre = strtok (row[0], "&") ;
			while ((nombre != NULL) && (encontrado == 0))
			{
				if (strcmp(nombre,usuario_peticion_4)==0)
				{
						encontrado = 1;
						printf ("LO ha encontrado? \n" );
						entra = 1;
				}
				else
				{
					nombre = strtok(NULL, "&");
				}
			}
			if (encontrado == 1)
			{
				sprintf(respuesta, "%s-%s",respuesta, jugadores);
			}
			i++;
			encontrado = 0;
		}
	
		int socket = DameSocketConectado(usuario_peticion_4);
		char resp_final15[400];
		if (entra == 1)
		{
			sprintf(resp_final15, "15/%s", respuesta);
			printf ("VALOR DE RESPUESTA FINAL  VALE =%s\n",resp_final15 );
			
			write(socket, resp_final15, strlen(resp_final15));
			return 0;
		}
		else
		{
			sprintf(resp_final15, "15/NO HA JUGADO NUNCA CON NADIE");
			
			
			write(socket, resp_final15, strlen(resp_final15));
			return 0;
		}
		
}

//code 16
int Resultado_partidas_que_jugue_con_persona(MYSQL *conn, char usuario_peticion_4[20], char persona_con_la_que_ha_jugado[20])
{
	int i = 1;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char query[100];
	
	char respuesta16[300];
	int encontrado = 0;
	char query2[100];
	int Id_maximo = 0;
	char jugadores[100];
	int entra = 0;
	char query3[100];
	char puntos[100];
	
	
	strcpy(query, "SELECT max(Id_partida) FROM partida_jugada");
	
	err=mysql_query (conn, query);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		Id_maximo = atoi (row[0]);
	}
	
	printf ("Id_maximo =%d \n",Id_maximo );
	
	while (i <= Id_maximo)
	{
		sprintf(query2, "SELECT nombre_jugadores FROM partida_jugada WHERE Id_partida = %d ", i);
		
		err=mysql_query (conn, query2);
		if (err!=0)
		{
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return -1;
		}
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		if (row == NULL)
			printf ("No se han obtenido datos en la consulta\n");
		else
		{
			
			//char *nombre = strtok (row[0], "&") ;
			
		}
		
		
		printf ("HAGO LA QUERY\n" );
		/*			resultado = mysql_store_result (conn);*/
		/*			row = mysql_fetch_row (resultado);*/
		
		printf ("VALOR DEL SELECT=%s\n",row[0] );
		strcpy(jugadores, row[0]);
		
		char *nombre = strtok (row[0], "&") ;
		while ((nombre != NULL) && (entra != 2))
		{
			if (strcmp(nombre,usuario_peticion_4)==0)
			{
				
				printf ("ENTRA EN EL PRIMERO? \n" );
				entra ++;
			}
			if (strcmp(nombre,persona_con_la_que_ha_jugado)==0)		
			{
				
				entra++;
				printf ("ENTRA EN EL SEGUNDO? \n" );
			}
			nombre = strtok(NULL, "&");
		}
		if (entra == 2)
		{
			printf ("hE ENTRADO EN EL ENTRA == 2 \n" );
			encontrado = 1;
			sprintf(query3, "SELECT puntos FROM partida_jugada WHERE Id_partida = %d ", i);
			
			err=mysql_query (conn, query3);
			if (err!=0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				return -1;
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				
				printf ("row[0] =%s \n",row[0] );
				
			}
			
			
			printf ("HAGO LA QUERY\n" );
			/*			resultado = mysql_store_result (conn);*/
			/*			row = mysql_fetch_row (resultado);*/
			
			printf ("VALOR DEL SELECT=%s\n",row[0] );
			strcpy(jugadores, row[0]);
			
			sprintf(respuesta16, "%s-%s",respuesta16, jugadores);
		}
		i++;
		
		entra = 0;
	}
	
	int socket = DameSocketConectado(usuario_peticion_4);
	char resp_final16[400];
	if (encontrado == 1)
	{
		sprintf(resp_final16, "16/%s", respuesta16);
		printf ("VALOR DE RESPUESTA FINAL  VALE =%s\n",resp_final16 );
		
		write(socket, resp_final16, strlen(resp_final16));
		
	}
	else
	{
		sprintf(resp_final16, "16/NO HAN JUGADO NUNCA JUNTOS");
		
		
		write(socket, resp_final16, strlen(resp_final16));
		
		
	}
}
//code 17
int Lista_partidas_periodo_tiempo(MYSQL *conn,char fecha1[10], char fecha2[10], char usuario_peticion_6[20] )
{
	int i = 0;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char query[100];
	
	char respuesta17[512];
	int encontrado = 0;
	char query2[100];
	int Id_maximo = 0;
	char jugadores[100];
	int entra = 0;
	char query3[100];
	char puntos[100];
	char resp_final17[512];
	
	int socket = DameSocketConectado(usuario_peticion_6);
	
	//sprintf(query, "SELECT nombre_jugadores FROM partida_jugada WHERE fecha >= %s AND fecha <= %s", fecha1, fecha2);
	sprintf(query, "SELECT nombre_jugadores FROM partida_jugada WHERE fecha BETWEEN '%s' AND '%s';", fecha1, fecha2);
	
	err=mysql_query (conn, query);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		
	}
	
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	
	while (row != NULL)
	{
		
		sprintf(respuesta17, "%s--%s", respuesta17, row[0]);
		row = mysql_fetch_row (resultado);
	}
	sprintf(resp_final17, "17/%s", respuesta17);
	printf ("VALOR DE RESPUESTA FINAL  VALE =%s\n",resp_final17 );
	
	write(socket, resp_final17, strlen(resp_final17));
	
	
}
void obtenerFechaActual(char* buffer, size_t buffer_size) {
	time_t t = time(NULL);
	struct tm* tm_info = localtime(&t);
	strftime(buffer, buffer_size, "%Y-%m-%d", tm_info); // Formato yyyy-mm-dd
}
int insertarPartidaEnDB(MYSQL *conn, Partida *partida, const char *jugadorespuntos, char los_usuarios_y_puntos[200]) {
	char query[1024];
	char nombre_jugadores[512] = "";
	char puntos[512] = "";
	printf("los_usuarios_y_puntos al principio = %s\n", los_usuarios_y_puntos);
	char jugadorespuntos_copy[strlen(jugadorespuntos) + 1];
	strcpy(jugadorespuntos_copy, jugadorespuntos);
	printf("jugadorespuntos_copy = %s\n", jugadorespuntos_copy);
	char nombre[20];
	char puntos_nombre[20];
	char nombre_array[200];
	char puntos_array[200];
	
	char token_nombre[100];
	char token_puntos[100];
	
	char *token = strtok(los_usuarios_y_puntos, "&");
	if (token == NULL)
	{
		*token = strtok(NULL, "&");
	}
	while( token != NULL)
	{
		 *token_nombre = strtok(token, "*");
		strcpy(nombre, token_nombre);
		
		sprintf(nombre_array, "%s&%s", nombre_array, nombre);
		
		 *token_puntos = strtok(NULL, "*");
		strcpy(puntos_nombre, token_puntos);
		
		sprintf(puntos_array, "%s&%s", puntos_array, puntos_nombre);
		
		 *token = strtok(NULL, "&");
	}
	printf("nombre_array = %s\n", nombre_array);
	printf("puntos_array = %s\n", puntos_array);
	
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al insertar partida: %s\n", mysql_error(conn));
		return 0;
	}
	printf("Partida insertada correctamente\n");
	printf("los_usuarios_y_puntos al final = %s\n", los_usuarios_y_puntos);
	return 1;
}
//Funcion principal del programa donde determinamos que es lo que ha pedido el cliente
void *handleClientRequest (void *arg) {
	
	int posicion;
	int *s;
	s= (int *) arg;
	posicion= *s;
	/*printf("valor posicion que es lo que se le pasa a handlerequest: %d\n",posicion);*/
	miLista.num = posicion;
	int clientSocket = miLista.conectados[posicion - 1].socket;
	char request[512];
	char response[512];
	
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
			.servidor = "shiva2.upc.es",
				.usuario = "root",
				.contrasena = "mysql",
				.base_datos = "MG5_Kahoot"
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
			
			char response5[512];
			
			char *usuario = strtok(NULL, "/");
			strcpy(nombre,usuario);
			char *contrasena = strtok(NULL, "/");
			registro(conn, usuario, contrasena);
			sprintf(response5, "5/%s", usuario);
						
			pthread_mutex_lock(&mutex); 
			int respuesta = AnadiraLista( &usuario, clientSocket);
			
			NotificarNuevaListaConectados();
			pthread_mutex_unlock(&mutex);
			
			write(miLista.conectados[posicion - 1].socket,response5, strlen(response5));
			printf ("Respuesta5: %s\n", response5);
		}else if (code ==  6) {
			//Mensaje en peticion: 6/invitado1*invitado2*...
			//Return en respuesta: 7/0 (Todo OK) ; 7/invitado_no_disponible1/... (si hay invitados que se han desconectado)
			char response6[512];
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
					strcpy(response6,"6/-1");
				}
				else{
					strcpy(response6,"6/0");					 
				}
			write(miLista.conectados[posicion - 1].socket,response6, strlen(response6));
				printf ("Respuesta6: %s\n", response6);
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
			int iden = atoi(p);
			p = strtok (NULL, "/");
			char invitados[512];
			char *c = strtok(NULL, "/");
			char host[100];
			strcpy(host,c);
			strcpy(invitados,p);
			contadorpartidas++;
			int identificador=contadorpartidas-1;
			
			Partida partida=crearPartida(invitados, identificador,host);
			EnviarComenzarPartida(numForm, invitados ,identificador,host);
			
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
		else if(code == 11) //aqui nos pasaran el id de la pregunta que quieren que pongamos en el kahoot
		{
/*			char *p1 = strtok(NULL, "/");*/
/*			int id_preg = atoi(p1);*/
			char *p1 = strtok(NULL, "/");
			int id_partida = atoi(p1);
			int resultado_query = pregunta_bd(conn, id_partida);
			
			
		}
		else if (code == 12)
		{
		
			char *p1 = strtok(NULL, "/");
			char usuario_puntos[512];
			strcpy (usuario_puntos,p1);
			char *p3 = strtok(NULL, "/");
			int id_partida = atoi(p3);
			char puntos[1000];
			sprintf(puntos,"12/%d/%s",id_partida,usuario_puntos);
			enviarpuntos(puntos,id_partida);
			listapartidas.partida[id_partida].contRespuestas++;
			int contrespuestas=listapartidas.partida[id_partida].contRespuestas;
			int personas=personas_partida(id_partida);
			printf("Numero de personas %d\n",personas);
			printf("Numero de contador: %d\n",contrespuestas);
			if(contrespuestas==personas)
			{char mensaje[512];
			sprintf(mensaje,"13/%d",id_partida);
			enviarpuntos(mensaje,id_partida);
			listapartidas.partida[id_partida].contRespuestas=0;
			}
			
		}
		else if (code == 13)
		{
			char *p3 = strtok(NULL, "/");
			int id_partida = atoi(p3);
			listapartidas.partida[id_partida].contRespuestas=0;
		}
		else if (code == 14)
		{
			char *usuario = strtok(NULL, "/");
			char *contrasena = strtok(NULL, "/");
			strcpy(nombre, usuario);
			
			// Verificar las credenciales en la base de datos
			if (verificarCredenciales(conn, nombre, contrasena)) {
				
				Baja(conn, nombre, contrasena);
					sprintf(response, "14/%s", nombre);
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
												
			}
			else 
			{
				strcpy(response, "14/99");
			}
			write(miLista.conectados[posicion - 1].socket,response, strlen(response));	
		}
		else if (code == 15)
		{
			char *p1 = strtok(NULL, "/");
			char usuario_peticion_4[512];
			strcpy (usuario_peticion_4,p1);
			
			int result = lista_jugadores_con_los_que_ha_jugado(conn, usuario_peticion_4);
			
		}
		else if (code == 16)
		{
			char *p1 = strtok(NULL, "/");
			char usuario_peticion_5[512];
			strcpy (usuario_peticion_5,p1);
			
			char *p2 = strtok(NULL, "/");
			char persona_con_la_que_ha_jugado[512];
			strcpy (persona_con_la_que_ha_jugado,p2);
			
			int resultado = Resultado_partidas_que_jugue_con_persona(conn, usuario_peticion_5, persona_con_la_que_ha_jugado);
		}
		else if (code == 17)
		{
			char *p1 = strtok(NULL, "/");
			char usuario_peticion_6[512];
			strcpy (usuario_peticion_6,p1);
			
			char fecha1[10];
			char *p2 = strtok(NULL, "/");
			strcpy (fecha1,p2);
			
			char fecha2[10];
			char *p3 = strtok(NULL, "/");
			strcpy (fecha2,p3);
			
			
			int resultado = Lista_partidas_periodo_tiempo(conn, fecha1, fecha2, usuario_peticion_6);
		}
		else if(code == 19)
		{
			
			char *i = strtok(NULL, "/");
			char id[512];
			strcpy(id,i);
			char respuesta[512];
			sprintf(respuesta, "19/%s",id);
			enviarchat(respuesta,atoi(id));
		}
		else if(code == 20)
		{
			char *p1 = strtok(NULL, "/");
			char usuario_puntos[512];			
			strcpy (usuario_puntos,p1);
			printf("usuario puntos___code20: %s \n", usuario_puntos);
			
			char string_resuelto[300];
			
			char *p3 = strtok(NULL, "/");
			int id_partida = atoi(p3);
			char puntos[1000];
			sprintf(puntos,"12/%d/%s",id_partida,usuario_puntos);
			enviarpuntos(puntos,id_partida);
			sprintf(listapartidas.partida[id_partida].puntos,"%s&%s",listapartidas.partida[id_partida].puntos,puntos);
			listapartidas.partida[id_partida].contTerminar++;
			int contterminar=listapartidas.partida[id_partida].contTerminar;
			
			sprintf(finaliza_partida_string, "%s&%s", finaliza_partida_string, usuario_puntos);
				
			int personas=personas_partida(id_partida);
			printf("Numero de personas %d\n",personas);
			printf("Numero de contador: %d\n",contterminar);
			if(contterminar==personas)
			{char mensaje[512];
			sprintf(mensaje,"20/%d",id_partida);
			enviarpuntos(mensaje,id_partida);
			listapartidas.partida[id_partida].contTerminar=0;
			obtenerFechaActual(listapartidas.partida[id_partida].fecha, sizeof(listapartidas.partida[id_partida]));
			char jugadorespuntos[10000];
			strcpy(jugadorespuntos,listapartidas.partida[id_partida].puntos);
			//hacer el insert
			printf("variable_global ===== %s\n", finaliza_partida_string);
			contterminar= 0;
			
			strcpy(string_resuelto,finaliza_partida_string);
			if (!insertarPartidaEnDB(conn, &listapartidas.partida[id_partida], jugadorespuntos, string_resuelto)) {
				fprintf(stderr, "No se pudo insertar la partida\n");
			}
			}
			finaliza_partida_string[0] = "\0";
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
		if (code !=0 && code != 5 && code != 4 && code != 7 && code != 10 && code !=8 && code != 6 && code != 11 && code !=12 && code != 15 && code != 16  && code != 17 && code !=19 && code != 20)
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
		.servidor ="shiva2.upc.es",
			.usuario = "root",
			.contrasena = "mysql",
			.base_datos =/*"MG5_KAHOOT_DEF"*/"MG5_Kahoot"
	};
	
	// Conectar a la base de datos utilizando la estructura de conexión
	MYSQL *conn = conectarBD(&conexion);
	if (conn == NULL) {
		// Si hay un error en la conexión, salir del programa
		return 1;
	}
	
	printf("Conexión a la base de datos establecida correctamente.\n");		
	int serverSocket;
	int puerto = 50022; //50023;
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
		/*printf("el socket en el for del main es %d \n", miLista.conectados[posicion_vector].socket);*/
		pthread_create (&thread, NULL,  &handleClientRequest,&posicion_vector);
		printf("%d \n", posicion_vector);
		posicion_vector++;
		
	}
	close(serverSocket);
	
	mysql_close(conn);
	
	return 0;
}
