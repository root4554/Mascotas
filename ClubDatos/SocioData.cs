using ClubObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClubDatos;
    
    public class SociosCSV : SaveData<Socio>
    {
        string _file2 = "../../RepositoriosCSV/Socios.csv";

        public void Guardar(List<Socio> misSocios)
        {
            List<string> data = new() { };
            misSocios.ForEach(Socio =>
            {
                var infoS = $"{Socio.SocioId},{Socio.NombreS},{Socio.Sexo}";
                data.Add(infoS);
            });
            File.WriteAllLines(_file2, data);
        }
        public List<Socio> Leer()
        {
            List<Socio> misSocios = new();
            var data = File.ReadAllLines(_file2).Where(row => row.Length > 0).ToList();
            data.ForEach(row =>
            {
                var campos = row.Split(",");
                var socio = new Socio(
                    socioID: campos[0],
                    nombreS: campos[1],
                    sexo: (Sexo)Enum.Parse((typeof(Sexo)), campos[2])
                );
                misSocios.Add(socio);
            });
            return misSocios;
        }

    }