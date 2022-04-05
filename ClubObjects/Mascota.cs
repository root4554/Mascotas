namespace ClubObjects;

  public enum Especie
{
    Perro,Gato,Hamster,tortuga
}

public class Mascota
{
        public string IDmascota {get;set;}
    public string NombreM {get; set;}

    public Especie Especie  {get; set;}
    public int Edad {get; set;}

    public Mascota(string idmascota,string nombreM,Especie especie,int edad){
        IDmascota = idmascota;
        NombreM = nombreM;
        Edad = edad;
        Especie = especie;
    }

    public override string ToString()=> $"{NombreM} {Especie} {Edad}";
}