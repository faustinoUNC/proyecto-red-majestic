using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tp_4
{
    internal class Program
    {
        //Funcion que genera un numero aleatorio
        public static float GenerateRandomTransaction()
            {
                Random rand = new Random();
                return (float)rand.NextDouble() * 1000;
            }



        //Static significa que la variable es compartida por todas las instancias de la clase y sirve para que la variable sea accesible desde cualquier parte del programa
        static string numeroTarjeta= "";


        ///////////////////////////MAIN///////////////////////////
        static void Main(string[] args)
        {

            RenderizarMenu();

            //validar que opcion sea un numero y que este dentro de las opciones permitidas
            int opcion = 0;
            bool esNumero = false;

            do
            {
                esNumero = int.TryParse(Console.ReadLine(), out opcion);
                if (!esNumero)
                {
                    Console.WriteLine("Debe ingresar un número");
                    Console.WriteLine("Presione cualquier teclapara continuar");
                    Console.ReadKey();
                }
                else if (opcion < 1 || opcion > 3)
                {
                    Console.WriteLine("La opción ingresada no es válida. Inténtelo nuevamente");
                    Console.WriteLine("Presione cualquier teclapara continuar");
                    Console.ReadKey();
                }

                Console.Clear();
                RenderizarMenu();

            } while (!esNumero || opcion < 1 || opcion > 3);// Se repetira mientras estas condiciones sean true



            //Ingresar el numero de tarjeta de credito
            Console.Clear();
            Console.WriteLine("Ingrese los 16 dígitos de su tarjeta de crédito");
            //validar que el string este entre 0 y 9 .lenght de la cadena 
            numeroTarjeta = Console.ReadLine();

            string empresa = numeroTarjeta.Substring(0, 4);//.Substring es un metodo de la clase string que permite obtener una subcadena de la cadena original , lo que se pasa como parametro es el indice de inicio y la cantidad de caracteres que se desea obtener


            
            //Validar la tarjeta de credito
            if (ValidarTarjeta(empresa, opcion))
            {
                switch (opcion)
                {
                    case 1:
                        mostrarUltimos4Digitos(numeroTarjeta);
                        ObtenerTransaccionesVisa(empresa,opcion);
                        break;
                    case 2:
                        mostrarUltimos4Digitos(numeroTarjeta);
                        ObtenerTransaccionesMastercard(empresa, opcion);
                        break;
                    case 3:
                        mostrarUltimos4Digitos(numeroTarjeta);
                        ObtenerTransaccionesDinersClub(empresa, opcion);
                        break;
                }
            }
            else
            {
                Console.WriteLine("El número de tarjeta ingresada no es válido. Inténtelo nuevamente más tarde.");
            }



            ///////////////////////////FUNCIONES///////////////////////////
           
             //funcion renderizar menu
            static void RenderizarMenu()
            {
                Console.WriteLine("Bienvenido a Red Majestic");
                Console.WriteLine("Operar con Visa - Ingrese 1");
                Console.WriteLine("Operar con Mastercard - Ingrese 2");
                Console.WriteLine("Operar con Diners Club - Ingrese 3");
                Console.WriteLine("Ingrese su opcion");
            }

            //Funcion que valida la tarjeta de credito
            static bool ValidarTarjeta(string empresa, int opcion)
            {
                switch (opcion)
                {
                    case 1://Retorna true si los primeros 4 digitos de la tarjeta son 4407 y coincide con la opcion 1
                        return empresa == "4407";
                    case 2://Retorna true si los primeros 4 digitos de la tarjeta son 3890 y coincide con la opcion 2
                        return empresa == "3890";
                    case 3://Retorna true si los primeros 4 digitos de la tarjeta son 7401 y coincide con la opcion 3
                        return empresa == "7401";
                    default://Retorna false si no es ninguna de las anteriores
                        return false;
                }
            }


            static void mostrarUltimos4Digitos(string numeroTarjeta)
            {
                Console.WriteLine("Movimientos de su cuenta Visa terminada en .." + numeroTarjeta.Substring(12,4));
            }



            //funcion que obtiene las transacciones de visa como array y mostrarlas con un while
            //Los arrays son colecciones de elementos que se pueden acceder por su indice y que tienen una cantidad fija de elementos
            static void ObtenerTransaccionesVisa(string empresa, int opcion)
            {
                float[] transacciones = new float[5];

                if (ValidarTarjeta(empresa, opcion))
                {

                    //cargo vector de 5 transacciones con ciclo for
                    for (int i = 0; i < transacciones.Length; i++)
                    {
                        transacciones[i] = GenerateRandomTransaction();
                    }

                    int j = 0;
                    while (j < transacciones.Length)
                    {
                        Console.WriteLine("Transacción N°" + (j + 1) + " - Monto: $" + transacciones[j]);
                        j++;
                    }
                }
                else
                {
                    Console.WriteLine("La tarjeta ingresada no es visa");
                }

            }

            //funcion que obtiene las transacciones de mastercard como lista y mostrarlas con un do while
            //Las listas son colecciones de elementos que se pueden acceder por su indice y que pueden tener cualquier cantidad de elementos
            static void ObtenerTransaccionesMastercard(string empresa, int opcion)
            {
                List<float> transacciones = new List<float>();

                if (ValidarTarjeta(empresa, opcion))
                {
                    //cargo la lista de transacciones con 5 transacciones
                    for (int i = 0; i < 5; i++)
                    {
                        transacciones.Add(GenerateRandomTransaction());
                    }

                    int j = 0;
                    do
                    {
                        Console.WriteLine("Transacción N°" + (j + 1) + " - Monto: $" + transacciones[j]);
                        j++;
                    } while (j < transacciones.Count);
                }
                else
                {
                    Console.WriteLine("Estarjeta no es mastercard");
                
                }

                
            }

            //funcion que obtiene las transacciones de Diners Club como diccionario y mostrarlas con un foreach
            //Los diccionarios son colecciones de elementos que se pueden acceder por su clave y que pueden tener cualquier cantidad de elementos
            static void ObtenerTransaccionesDinersClub(string empresa, int opcion)
            {
                Dictionary<int, float> transacciones = new Dictionary<int, float>();

                if (ValidarTarjeta(empresa, opcion))
                {
                    //cargo el diccionario de transacciones con 5 transacciones
                    for (int i = 0; i < 5; i++)
                    {
                        transacciones.Add(i, GenerateRandomTransaction()); //El primer parametro es la clave que hace referencia a la transaccion y el segundo parametro es el valor que hace referencia al monto de la transaccion
                    }

                    foreach (KeyValuePair<int, float> transaccion in transacciones)
                    {
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Estarjeta no es Dinners Club");
                }

                

            }
        }
    }
}
