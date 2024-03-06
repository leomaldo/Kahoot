#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>

int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9000);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el c?digo de la petici?n
			char nombre[20];

			if (codigo !=0)
			{	
				//Establecemos conexión con la base de datos
                MYSQL *conn = mysql_init(NULL);
				if (conn == NULL) {
					fprintf(stderr, "Error al inicializar la conexion: %s\n", mysql_error(conn));
					return 1;
				}
				if (mysql_real_connect(conn, "localhost", "root", "mysql", "Kahoot", 0, NULL, 0) == NULL) {
					fprintf(stderr, "Error al conectarse a la base de datos: %s\n", mysql_error(conn));
					mysql_close(conn);
					return 1;
				}
	
				            printf("Conexion exitosa a la base de datos Kahoot\n");
							// Mostramos el codigo
							printf ("Codigo: %d\n", codigo);
			}

			if (codigo ==0) //petici?n de desconexi?n
				terminar=1;
			else if (codigo ==1) //piden la máxima puntuación
			    {

				//Aqui pondriamos nuestra respuesta de la maxima puntuacion
				sprintf (respuesta,"%d",strlen (nombre));
				 }
			else if (codigo == 2) //piden el jugador con mas puntos
			     {

       			 }
			else if (codigo == 3) //piden la partida con menos preguntas correctas
			     {

       			 }
			else if (codigo == 4) //quieren logearse
			     {

       			 }
			else  //quieren registrarse
			     {

       			 }
						
			if (codigo !=0)
			{				
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 		
	}
}