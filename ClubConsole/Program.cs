using ClubObjects;
using GestionApp;
using ClubDatos;

var repoMasc = new MascotasCSV();
var repoSocio = new SociosCSV();
var view = new vista();
var sistema = new Gestion(repoSocio,repoMasc);
var controlador = new Controlador(view,sistema);
controlador.Run();
