//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using System.Data;
//using System.Windows.Forms;
//using MySql.Data.MySqlClient;


//namespace ElAustral
//{
//    internal class ConexionDB
//    {

//        public void MySqlConnector()
//        {

//        }

//        public MySqlConnection GetSQLConnection()
//        {
//            string server = "localhost";
//            string user = "root";
//            string pwd = "123";
//            string database = "austral";
//            string connectionString = $"server={server};database={database};uid={user};password={pwd};authMechanism=auth_gssapi_client";

//            MySqlConnection connection = new MySqlConnection(connectionString);
//            connection.Open(); // Intenta abrir la conexión

//            if (connection.State == ConnectionState.Open)
//            {
//                return connection; // Si la conexión se abrió correctamente, devuelve la conexión
//            }
//            else
//            {
//                // Manejar el error de conexión
//                MessageBox.Show("Error al conectar a la base de datos");
//                return null; // O puedes lanzar una excepción
//            }
//        }

//        public Productos GetId_producto(string id)
//        {
//            var sqlConnection = GetSQLConnection();
//            sqlConnection.Open();
//            var command = sqlConnection.CreateCommand();
//            command.CommandText = "SELECT * FROM productos";
//            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
//            Productos productos = new Productos();
//            while (reader.Read())
//            {
//                productos.id_producto = reader.GetInt32("id_producto");
//                productos.nombre_producto = reader.GetString("nombre_producto");
//                productos.descripcion = reader.GetString("descripcion");
//                productos.precio_venta = reader.GetInt32("precio_venta");
//                productos.categoria = reader.GetString("categoria");
//                productos.stock = reader.GetInt32("stock");
//            }
//            return productos;
//        }
//    }
//}
