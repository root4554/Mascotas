using ClubObjects;

namespace ClubDatos;

 public class MascotasCSV : SaveData<Mascota>
    {

        string _file1 = "../CSVrepository/Mascotas.csv";


        //Persistencia

        public void Guardar(List<Mascota> misMascotas)
        {
            List<string> data = new() { };
            misMascotas.ForEach(Mascota =>
            {
                var infoM = $"{Mascota.IDmascota},{Mascota.NombreM},{Mascota.Especie},{Mascota.Edad}";
                data.Add(infoM);

            });
            File.WriteAllLines(_file1, data);
        }
        public List<Mascota> Leer()
        {
            List<Mascota> misMascotas = new();
            var data = File.ReadAllLines(_file1).Where(row => row.Length > 0).ToList();
            data.ForEach(row =>
            {
                var campos = row.Split(",");
                Mascota mascota = new Mascota(
                    idmascota: campos[0],
                    nombreM: campos[1],
                    especie: (Especie)Enum.Parse((typeof(Especie)), campos[2]),
                    edad: int.Parse(campos[3])
                );
                misMascotas.Add(mascota);
            });

            return misMascotas;
        }


    }
