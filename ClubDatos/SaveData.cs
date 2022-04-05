namespace ClubDatos;
    public interface SaveData<T>
    {

        public void Guardar(List<T> datos);
        public List<T> Leer();
    }

