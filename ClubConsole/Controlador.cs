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

        
        public Controlador(vista vista, Gestion businessLogic)
        {
            _vista = vista;
            _sistema = businessLogic;
            _casosDeUso = new Dictionary<string, Action>(){
                {"Alta por una persona", DarDeAltaS},
                {"Dar de baja a un Socio",DarDeBajaS},
                {"Mostrar lista de Socios",VerSocios},
                {"Anadir una Mascota",AñadirMascota},
                {"Mostrar mascotas",VerMascotasDelClub},
                //{"Ver las Mascotas de cada Socio",VerMascotasSocio},
               // {"Comprar Mascota",Comprar},
               // {"Poner en venta Mascota",PonerEnVentaMascotaSocio},
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
                //var dni = _vista.TryObtenerDatoDeTipo<string>("DNI del Socio");
                var sexo = _vista.TryObtenerDatoDeTipo<Sexo>("Sexo del Socio ( H-M-NB )");
                Socio nuevo = new Socio
                (
                    socioID: SocioID,
                    nombreS: nombre,
                    sexo : sexo
                    
                );

                _sistema.NuevoSocio(nuevo);
            }catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}");
                }
                finally{
                     _vista.Mostrar("Socio añadido!!!\nNo te olvides de añadir sus mascotas .");
                }


        }

        private void DarDeBajaS()
        {
           Socio p;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para dar de baja ");     
            if(p.NombreS!="Club"){           
            _sistema.BorrarSocio(p);

            aux=p;

            _usoSocio = new Dictionary<string, Action>(){
               // {"Eliminar mascotas", EliminarMascota},
               // {"Poner en venta mascotas",PonerEnVentaMascota}       

            };
             var menu2 = _usoSocio.Keys.ToList<String>();
              try
                {
                    //Limpiamos
                    _vista.LimpiarPantalla();
                    //Menu
                    var key = _vista.TryObtenerElementoDeLista("Que quieres hacer con sus mascotas",menu2,"Selecciona una opcion");
                    _vista.Mostrar("");
                    //Ejecucion de la opcion escogida
                    _usoSocio[key].Invoke();
                    //Fin
                   // _vista.MostrarYReturn("Pulsa <Return> para continuar");

                }
                catch { return; }
            }else{
                _vista.Mostrar("Este socio no se puede borrar.\nEs la cuenta del Club.");
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
                var especie = _vista.TryObtenerElementoDeLista<Especie>("Especie de mascota ", _vista.EnumToList<Especie>(),"Selecciona uno ");
                var edadM = _vista.TryObtenerDatoDeTipo<string>("Edad de la Mascota ");
               // var dni = _vista.TryObtenerDatoDeTipo<string>("DNI del dueño  ");
                //var venta = _vista.TryObtenerDatoDeTipo<string>("Esta en venta : True/False ");

                Mascota nueva = new Mascota
                (
                    idmascota:idMascota,
                    nombreM: nombre,
                    especie : especie,
                    edad : int.Parse(edadM)
                   // DNIPr : dni,
                    //enVenta: venta
                );

                _sistema.NuevaMasc(nueva);
            }catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}");
                }

        }

      /*  private void VerMascotasSocio() 
        {
             Socio p;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio para ver sus Mascotas ");   

            _vista.LimpiarPantalla(); 
            if(_sistema.EncontrarMascotasPorDNI(p).Count()==0)
            {
                 _vista.Mostrar("El socio "+p.Nombre+" no tiene Mascotas registradas\n\n");
                
            }else{

                _vista.MostrarListaEnumerada<Mascota>("Lista De Mascotas de "+p.Nombre.ToString(),  _sistema.EncontrarMascotasPorDNI(p));

            }


        }*/

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
                    var key = _vista.TryObtenerElementoDeLista("Como quieres ver ordenadas las Mascotas",menu3,"Selecciona una opcion");
                    _vista.Mostrar("");
                    //Ejecucion de la opcion escogida
                    _usoMascota[key].Invoke();
                    //Fin
                    //_vista.MostrarYReturn("Pulsa <Return> para continuar");

                }
                catch { return; }

        }
        public void OrdenarMascotaEdad()
        {         
            _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por edad ",_sistema.misMascotas.OrderByDescending(mascota => mascota.Edad).ToList());            
        }
        private void OrdenarMascotaEspecie()
        {
            _vista.MostrarListaEnumerada<Mascota>("Mascotas ordenadas por especie ",_sistema.misMascotas.OrderByDescending(mascota => mascota.Especie).ToList());
        }
       /* private void PonerEnVentaMascota(){
            _sistema.VenderMascota(aux);
            _vista.Mostrar("\nMascotas en venta");
        }*/
        /*private void PonerEnVentaMascotaSocio(){
             Socio p;
             Mascota m;

            p = _vista.TryObtenerElementoDeLista("Socios del Club de Mascotas       ", _sistema.misSocios, "Selecciona un Socio ");   
            _vista.LimpiarPantalla(); 
            if(_sistema.EncontrarMascotasPorDNI(p)==null)
            {
                _vista.Mostrar("Este Socio no tine Mascotas");
            }else{

            m =_vista.TryObtenerElementoDeLista("Lista De Mascotas de "+p.Nombre.ToString(),_sistema.EncontrarMascotasPorDNI(p),"Selecciona una mascota");
           // (_sistema.EncontrarMascotasPorDNI(p)).ForEach(mascota =>mascota.enVenta = "true" );
            m.enVenta="true";
            _sistema.ActualizarRepoMasc();
            _vista.Mostrar("\nMascotas en venta");
            }

        }*/


        /*private void EliminarMascota(){
            _sistema.BorrarMascotasDeSocio(aux);    
            _vista.Mostrar("Las Mascotas fueron borradas del sistema\nGracias . ");  
        }*/
      /*  public void Comprar()
        {        
            Mascota masc;
            masc = _vista.TryObtenerElementoDeLista("Lista de Mascotas en venta ", _sistema.VerMascotasEnVenta(), "Selecciona una Mascota que quieras comprar");
            var dniNuevo = _vista.TryObtenerDatoDeTipo<string>("Introduce tu dni");//esto lo hago para captar nuevos soocios para el club XD
            if (_sistema.misSocios.Find(socio => dniNuevo.Equals(socio.DNI)) == null)
            {
                _vista.Mostrar("Si quieres comprar un Mascota primero debes hacerte Socio\nGracias");
            }else
            {
                masc.DNIPr = dniNuevo;
                masc.duenio.Nombre = (_sistema.misSocios.Find(socio => dniNuevo.Equals(socio.DNI))).Nombre;
                masc.enVenta = "false";
                _sistema.ActualizarRepoMasc();
            }  
            _vista.Mostrar("\nMascota comprada!!\n");         

        }   */
        
        
    }