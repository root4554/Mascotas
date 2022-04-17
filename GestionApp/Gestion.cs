using ClubObjects;
using ClubDatos;

namespace GestionApp;
public class Gestion
{
    public Gestion(SociosCSV repoS, MascotasCSV repoM)
    {
        RepoSocio = repoS;
        RepoMasc = repoM;
        misSocios = RepoSocio.Leer();
        misMascotas = RepoMasc.Leer();
    }
    MascotasCSV RepoMasc;
    SociosCSV RepoSocio;
    public List<Mascota> misMascotas { get; set; } = new();
    public List<Socio> misSocios { get; set; } = new();

    //--------------Gestion Socios----------------------        
    public void NuevoSocio(Socio p)
    {
        misSocios.Add(p);
        RepoSocio.Guardar(misSocios);
    }

    public Socio EncontrarSocioPorID(string Id)
        => misSocios.Find(socio => Id.Equals(socio.SocioId));

    public void BorrarSocio(Socio p)
    {
        misSocios.Remove(p);
        RepoSocio.Guardar(misSocios);

    }

    //-------------------Gestion Mascotas--------------   

    public void NuevaMasc(Mascota p)
    {
        misMascotas.Add(p);
        RepoMasc.Guardar(misMascotas);
    }
    public void ActualizarRepoMasc()
    {
        RepoMasc.Guardar(misMascotas);
    }
    public List<Mascota> EncontrarMascotasPorID(Socio uno)
        => misMascotas.FindAll(mascota => uno.SocioId.Equals(mascota.IDPr));
    public void BorrarMascotasDeSocio(Socio uno)
    {
        misMascotas.RemoveAll(mascota => uno.SocioId.Equals(mascota.IDPr));
        RepoSocio.Guardar(misSocios);
        RepoMasc.Guardar(misMascotas);


    }

}
