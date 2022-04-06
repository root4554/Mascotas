using ClubObjects;
using ClubDatos;

namespace GestionApp;
public class Gestion
{
    public Gestion(SociosCSV repoS, MascotasCSV repoM){
           RepoSocio=repoS;
           RepoMasc=repoM;
            misSocios = RepoSocio.Leer();
            misMascotas = RepoMasc.Leer();
          //  misMascotas.ForEach( mascota => mascota.duenio = EncontrarSocioPorDNI(mascota.DNIPr));

        }
        MascotasCSV RepoMasc;
        SociosCSV RepoSocio;
        public List<Mascota> misMascotas {get;set;} = new();
        public List<Socio> misSocios {get;set;}= new();
//--------------------------------------------------
//--------------Gestion Socios----------------------        
        public void NuevoSocio(Socio p){
            misSocios.Add(p);
            RepoSocio.Guardar(misSocios);
        }
        public void QuitarSocio(Socio p){
            misSocios.Remove(p);
        }

        public Socio EncontrarSocioPorDNI(string Id)
            => misSocios.Find(socio => Id.Equals(socio.SocioId));

        public void BorrarSocio(Socio p)
        {
            misSocios.Remove(p);
            RepoSocio.Guardar(misSocios);

        }
            
//-------------------Gestion Mascotas--------------   

        public void NuevaMasc(Mascota p){
            misMascotas.Add(p);
            RepoMasc.Guardar(misMascotas);
        }
        public void ActualizarRepoMasc(){
             RepoMasc.Guardar(misMascotas);
        }
        /*public List<Mascota> EncontrarMascotasPorID(Socio uno)
            => misMascotas.FindAll(mascota => uno.SocioId.Equals(mascota.DNIPr));*/        
        public void BorrarMascota( Mascota p)
        {
            misMascotas.Remove(p);
            RepoMasc.Guardar(misMascotas);

        }
        
        /*public void BorrarMascotasDeSocio(Socio uno)
        {
            misMascotas.RemoveAll(mascota => uno.DNI.Equals(mascota.DNIPr));
            RepoSocio.Guardar(misSocios);
            RepoMasc.Guardar(misMascotas);


        }*/
       /* public void VenderMascota(Socio uno){

           EncontrarMascotasPorDNI(uno).ForEach( mascota => mascota.enVenta = "true");  
           EncontrarMascotasPorDNI(uno).ForEach( mascota => mascota.DNIPr = "87654321C");  
           ActualizarRepoMasc();     

        }*/
        /*public List<Mascota> VerMascotasEnVenta()
            => misMascotas.FindAll(mascota => mascota.enVenta == "true");    */  

    
}
