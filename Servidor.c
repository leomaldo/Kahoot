#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>

typedef struct {
	int id_partida;
	int id_usuario;
	int preguntas_correctas;
} Partida;

typedef struct {
	int id_usuario;
	char nombre_usuario[100];
} Jugador;

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

void registro(MYSQL *conn, const char *usuario, const char *contrasena) {
	char query[100];
	sprintf(query, "INSERT INTO usuarios (usuario, contrasena) VALUES ('%s', '%s')", usuario, contrasena);
	
	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error al insertar datos: %s\n", mysql_error(conn));
	} else {
		printf("Registro exitoso para usuario: %s\n", usuario);
	}
}

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


void handleClientRequest(int clientSocket, MYSQL *conn) {
	char request[512];
	char response[512];
	
	while (1) {
		int ret = read(clientSocket, request, sizeof(request));
		if (ret <= 0) {
			printf("Cliente desconectado\n");
			break;
		}
		
		request[ret] = '\0';
	int code = atoi(strtok(request, "/"));
	
	
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
		char *usuario = strtok(NULL, "/");
		char *contrasena = strtok(NULL, "/");
		
		registro(conn, usuario, contrasena);
		
		sprintf(response, "Registro exitoso para usuario: %s", usuario);
	}else if (code == 4) {
		char *usuario = strtok(NULL, "/");
		char *contrasena = strtok(NULL, "/");
		
		// Verificar las credenciales en la base de datos
		if (verificarCredenciales(conn, usuario, contrasena)) {
			sprintf(response, "Inicio de sesión exitoso para usuario: %s", usuario);
		}else {
			sprintf(response, "Credenciales incorrectas");
		}
	} else {
		sprintf(response, "Código no válido");
	}

	
	write(clientSocket, response, strlen(response));
	}
}
int main() {
	MYSQL *conn = mysql_init(NULL);
	
	if (conn == NULL) {
		fprintf(stderr, "Error al inicializar la conexión: %s\n", mysql_error(conn));
		return 1;
	}
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "Kahoot", 0, NULL, 0) == NULL) {
		fprintf(stderr, "Error al conectarse a la base de datos: %s\n", mysql_error(conn));
		mysql_close(conn);
		return 1;
	}
	printf("Conexión exitosa a la base de datos Kahoot\n");
	
	int serverSocket, clientSocket;
	struct sockaddr_in serverAddr, clientAddr;
	socklen_t addrLen = sizeof(struct sockaddr_in);
	
	serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	
	memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	serverAddr.sin_port = htons(9052);
	bind(serverSocket, (struct sockaddr *) &serverAddr, sizeof(serverAddr));
	listen(serverSocket, 3);
	
	while (1) {
		printf("Esperando conexiones...\n");
		
		int clientSocket = accept(serverSocket, (struct sockaddr *) &clientAddr, &addrLen);
		
		printf("Cliente conectado\n");
		
		handleClientRequest(clientSocket, conn);
		
		close(clientSocket);
	}
	close(serverSocket);
	
	mysql_close(conn);
	
	return 0;
}
