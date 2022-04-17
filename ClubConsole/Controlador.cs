using ClubObjects;
using GestionApp;



namespace ClubConsole;

class Controlador
{
    private vista _vista;

    private Gestion _sistema;
    private Dictionary<string, Action> _casosDeUso;

    private Dictionary<string, Action> _usoSocio;

    public Socio aux;

    private Dictionary<string, Action> _usoMascota;


    public Controlador(vista vista, Gestion gestionDeClub)
    {
        _vista = vista;
        _sistema = gestionDeClub;
        _casosDeUso = new Dictionary<string, Action>(){
                {"Alta por una persona", DarDeAltaS},
                {"Dar de baja a un Socio",DarDeBajaS},
                {"Mostrar lista de Socios",VerSocios},
                {"Anadir una Mascota",AñadirMascota},
                {"Mostrar mascotas",VerMascotasDelClub},
                {"Ver las Mascotas de cada Socio",VerMascotasSocio},
                {"Salir de  programa",Salir}

            };
    }
    // === Ciclo de la aplicacion ===
    public void Run()
    {
        _vista.LimpiarPantalla();
        //Acceso a las Claves del diccionario
        var menu = _casosDeUso.Keys.ToList<String>();

        while (true)
            try
            {
                //Limpiamos
                _vista.LimpiarPantalla();
                //Menu
                var key = _vista.TryObtenerElementoDeLista("Club de Mascotas", menu, "Selecciona una opcion ");
                _vista.Mostrar("");
                //Ejecucion de la opcion escogida
                _casosDeUso[key].Invoke();
                //Fin
                _vista.MostrarYReturn("Entrar para volver al menu principal");

            }
            catch { return; }
    }
    public void Salir()
    {
        var key = "fin";
        _vista.Mostrar("Has salido de programa");

        _casosDeUso[key].Invoke();
    }


    // === Casos De Uso ===
    /*-------------------casos de uso socios-----------------*/
    private void DarDeAltaS()
    {
        try
        {
            var SocioID = _vista.TryObtenerDatoDeTipo<string>("ID del Socio");
            var nombre = _vista.TryObtenerDatoDeTipo<string>("Nombre del Socio");
            var sexo = _vista.TryObtenerDatoDeTipo<Sexo>("Sexo del Socio ( H-M )");
            Socio nuevo = new Socio
            (
                socioID: SocioID,
                nombreS: nombre,
                sexo: sexo
            );

            _sistema.NuevoSocio(nuevo);
        }
        catch (Exception e)
        {
            _vista.Mostrar($"UC: {e.Message}");
        }
        finally
        {
            _vista.Mostrar("Socio añadido !");
        }
    }


    private void DarDeBajaS()
    {
        Socio p;

        p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para dar de baja ");
        if (p.NombreS != "Club")
        {
            _sistema.BorrarSocio(p);

            //aux = p;
            try
            {
                //Limpiamos
                _vista.LimpiarPantalla();
                //Fin1
                _sistema.BorrarMascotasDeSocio(p);
                _vista.Mostrar("Las Mascotas del Socio son eliminadas");

            }
            catch { return; }
        }
        else
        {
            _vista.Mostrar("Este socio no se puede borrar.");
        }
    }
    private void VerSocios()
    {
        _vista.MostrarListaEnumerada<Socio>("Lista De Socios", _sistema.misSocios);

    }

    /*------------casos de uso mascotas-----------*/
    public void AñadirMascota()
    {
        try
        {
            var idMascota = _vista.TryObtenerDatoDeTipo<string>("el ID de la Mascota ");
            var nombre = _vista.TryObtenerDatoDeTipo<string>("Nombre de la Mascota ");
            var especie = _vista.TryObtenerElementoDeLista<Especie>("Especie de mascota ", _vista.EnumToList<Especie>(), "Selecciona un Especie ");
            var edadM = _vista.TryObtenerDatoDeTipo<string>("Edad de la Mascota ");
            var IDPrsona = _vista.TryObtenerDatoDeTipo<string>("id del Socio  ");

            Mascota nueva = new Mascota
            (
                idmascota: idMascota,
                nombreM: nombre,
                especie: especie,
                edad: int.Parse(edadM),
                IDPersona: IDPrsona
            );

            _sistema.NuevaMasc(nueva);
        }
        catch (Exception e)
        {
            _vista.Mostrar($"UC: {e.Message}");
        }
    }

    private void VerMascotasSocio()
    {
        Socio p;

        p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para ver sus Mascotas ");

        _vista.LimpiarPantalla();
        if (_sistema.EncontrarMascotasPorID(p).Count() == 0)
        {
            _vista.Mostrar("El socio " + p.NombreS + " no tiene ningun Mascota\n");

        }
        else
        {

            _vista.MostrarListaEnumerada<Mascota>("Lista De Mascotas de " + p.NombreS.ToString(), _sistema.EncontrarMascotasPorID(p));

        }


    }

    private void VerMascotasDelClub()
    {
        _usoMascota = new Dictionary<string, Action>(){
                {"Ver ordenadas por Edad ", OrdenarMascotaEdad},
                {"Ver ordenadas por Especie",OrdenarMascotaEspecie}

            };
        var menu3 = _usoMascota.Keys.ToList<String>();
        try
        {
            //Limpiamos
            _vista.LimpiarPantalla();
            //Menu
            var key = _vista.TryObtenerElementoDeLista("Como quieres ver ordenadas las Mascotas", menu3, "Selecciona una opcion");
            _vista.Mostrar("");
            //Ejecucion de la opcion escogida
            _usoMascota[key].Invoke();
        }
        catch { return; }

    }
    public void OrdenarMascotaEdad()
    {
        _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por edad ", _sistema.misMascotas.OrderByDescending(mascota => mascota.Edad).ToList());
    }
    private void OrdenarMascotaEspecie()
    {
        _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por especie ", _sistema.misMascotas.OrderByDescending(mascota => mascota.Especie).ToList());
    }

}