using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace EspacioKanban;


public class UsuarioRepository : IUsuarioRepository {
    //private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
    private string cadenaConexion;
    
    public UsuarioRepository(string CadenaDeConexion)
    {
        cadenaConexion = CadenaDeConexion;
    }
    
    public void AddUsuario(Usuario usuario){
        var query = @"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre_de_usuario, @contrasenia, @rol);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateUsuario(int idUsuario, Usuario usuario){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Usuario SET nombre_de_usuario = @nombre, contrasenia = @contrasenia, rol = @rol WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@nombre",usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Usuario> GetAllUsuarios(){
        var query = @"SELECT * FROM Usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }
        if (usuarios == null)
        {
            throw new Exception("Lista de usuario No creada");
        }
        return usuarios;
    }

    public Usuario GetUsuario(int idUsuario){
        var query = @"SELECT * FROM Usuario WHERE id = @idUsuario;";
        Usuario usuario = new Usuario();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString(); //tiene que ir?
                    usuario.Rol = usuario.Rol = reader["rol"].ToString();
                }
            }
            connection.Close();
        }
        if (usuario == null)
        {
            throw new Exception("Usuario no creado");
        }
        return usuario;
    }
    public void DeleteUsuario(int idUsuario){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Usuario WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}