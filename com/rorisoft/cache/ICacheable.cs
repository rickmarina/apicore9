namespace com.rorisoft.cache
{
    /// <summary>
    ///  Las clases deben implementar un método que devuelva un id único en base a sus atributos: 
    ///    Ejemplo:
    ///String objstr = JsonFactory.serializeObject(this);
    ///        return Utils.md5(objstr);
    ///    
    /// </summary>
    public interface ICacheable
    {
        string _cacheName { get; set; }
        string getHash();

        string getToken(); //El token será una combinación entre cacheName_Hash
    }

    public interface ICacheL2
    {
        string estrategiaMejorPrimero();

    }
}
