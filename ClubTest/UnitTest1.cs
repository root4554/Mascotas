using Xunit;

namespace GestionApp
{
    public class FakeRepository : ClubDatos.SaveData<Mascota>
    {
        public void Guardar(List<Mascota> misMascotas)
        {
            throw new NotImplementedException();
        }
        public List<Mascota> Leer()
        {
            throw new NotImplementedException();
        }
    }
    public class FakeRepository2 : ClubDatos.SaveData<Socio>
    {
        public void Guardar(List<Socio> misSocios)
        {
            throw new NotImplementedException();
        }
        public List<Socio> Leer()
        {
            throw new NotImplementedException();
        }
    }
}

namespace GestionApp;
using ClubDatos;
public class UnitTest1
{
    [Fact]
    public void NuevoSociotest()
    {
        Socio s = new Socio
        {
            NombreS = "Anass",
            Sexo = 'H'
        };
        var sistema = new Gestion(new ClubDatos.SaveData<Socio>.FakeRepository());
        var cuenta = sistema.misSocios.Count;
        // When
        sistema.NuevoSocio(s);
        var cuentaMas1 = sistema.misSocios.Count;
        // Then
        Assert.Equal(cuenta + 1, cuentaMas1);
    }

    [Fact]
    public void QuitarSociotest()
    {
        Socio s1 = new Socio
        {
            NombreS = "Maroua",
            Sexo = 'M'
        };
        var sistema = new Gestion(new ClubDatos.SaveData<Socio>.FakeRepository());

        //when
        var resultado1 = sistema.misSocios.Remove(s1);
        // Then
        Assert.Equal(misSocios.DarDeBajaS, resultado1);
    }

    [Fact]
    public void NuevoMascotatest()
    {
        Mascota s = new Mascota
        {
            nombreM = "Alex",
            especie = "Perro",
            edad = 2
        };
        var sistema = new Gestion(new ClubDatos.SaveData<Mascota>.FakeRepository());
        var mascota = sistema.misMascotas.Count;
        // When
        sistema.NuevoSocio(s);
        var mascotaMas1 = sistema.misMascotas.Count;
        // Then
        Assert.Equal(mascota + 1, mascotaMas1);
    }

    [Fact]
    public void BorrarMascotasDeSociotest()
    {
        Socio s1 = new Socio
        {
            socioID = "id4",
            NombreS = "Maroua",
            Sexo = 'M'
        };
        Mascota m = new Mascota
        {
            nombreM = "toti",
            especie = "tortuga",
            edad = 23,
            IDPersona = "id4"
        };
        var sistema = new Gestion(new ClubDatos.SaveData<Mascota>.FakeRepository());

        // When
        sistema.BorrarMascotasDeSocio(s);
        var mascotaMenos = sistema.RemoveAll(mascota => p.SocioId.Equals(m.IDPr));
        // Then
        Assert.Equal(mascotaMenos, m);
    }
}