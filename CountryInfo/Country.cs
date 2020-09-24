/// <summary>
/// Класс для десериализации JSON-ответа от API
/// </summary>
public class Country
{
    public string name { get; set; }
    public string capital { get; set; }
    public string region { get; set; }
    public int population { get; set; }
    public double area { get; set; }
    public string numericCode { get; set; }
}
