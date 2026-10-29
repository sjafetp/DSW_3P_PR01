using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class MisionService : IMisionService
{
    private readonly IMisionRepository _repository;

    public MisionService(IMisionRepository repository)
    {
        _repository = repository;
    }

    public List<Mision> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Mision? ObtenerPorId(int id)
    {
        List<Mision> Misiones = _repository.ObtenerTodas();
        if(!Misiones.Any(m => m.Id == id))
        {
            return null;
        }

        return _repository.ObtenerPorId(id);
    }
    
    //No se deberán permitir modificaciones o eliminaciones de misiones inexistentes.
    //Todas las misiones nuevas deberán iniciar como no completadas.
    public Mision Agregar(Mision nueva)
    {
        nueva.Completada = false;

        if(string.IsNullOrWhiteSpace(nueva.Nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if(string.IsNullOrWhiteSpace(nueva.Dificultad))
            throw new ArgumentException("La dificultad es obligatoria");

        string nuevaM = nueva.Dificultad.Trim();
        if (!nuevaM.Equals("Fácil", StringComparison.OrdinalIgnoreCase) && 
            !nuevaM.Equals("Media", StringComparison.OrdinalIgnoreCase) && 
            !nuevaM.Equals("Difícil", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("La dificultad solo puede ser: Fácil, Media o Difícil");
        }

        if(nueva.RecompensaXP < 0)
            throw new ArgumentException("La recompensa no puede ser negativa");
        
        return _repository.Agregar(nueva);
    }

    public bool Actualizar(int id, Mision actualizada)
    {
        if(_repository.ObtenerPorId(id) == null)
            throw new ArgumentException("La misión no existe");

        if(string.IsNullOrWhiteSpace(actualizada.Nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if(string.IsNullOrWhiteSpace(actualizada.Dificultad))
            throw new ArgumentException("La dificultad es obligatoria");

        string actualizadaM = actualizada.Dificultad.Trim();
        if (!actualizadaM.Equals("Fácil", StringComparison.OrdinalIgnoreCase) && 
            !actualizadaM.Equals("Media", StringComparison.OrdinalIgnoreCase) && 
            !actualizadaM.Equals("Difícil", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("La dificultad solo puede ser: Fácil, Media o Difícil");
        }

        if(actualizada.RecompensaXP < 0)
            throw new ArgumentException("La recompensa no puede ser negativa");

        return _repository.Actualizar(id, actualizada);
    }

    public bool Eliminar(int id)
    {
        if(_repository.ObtenerPorId(id) == null)
            throw new ArgumentException("La misión no existe");
            
        return _repository.Eliminar(id);
    }
}