namespace ClubObjects;

public enum Sexo
{
    H,M
}
public class Socio
{
        public string SocioId{get; set;}
    public string NombreS {get; set;}
    public Sexo Sexo {get; set;}

    public Socio(string socioID ,string nombreS,Sexo sexo){
        SocioId = socioID;
        NombreS = nombreS;
        Sexo = sexo;
    }

    public override string ToString()=> $"{SocioId} {NombreS} {Sexo}";
}
