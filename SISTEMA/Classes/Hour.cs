namespace sistema.Classes
{
  public class HOUR
  {

    public string HOURS(string? noturno, string? diurno)
    {


      string input = noturno!;
      string[] result_noturno = input.Split(':');
      string input2 = diurno!;
      string[] result_diurno = input2.Split(':');

      var hour_norutno = new TimeSpan(Convert.ToInt32(result_noturno[0]), Convert.ToInt32(result_noturno[1]), 0);
      var hour_diurno = new TimeSpan(Convert.ToInt32(result_diurno[0]), Convert.ToInt32(result_diurno[1]), 0);
      var total = hour_norutno + hour_diurno;

      return Convert.ToString(total);
    }

    public string HOURSVFR(string? vfr, string? ifr, string? ifr_capota)
    {


      string input = vfr!;
      string[] result_vfr = input.Split(':');

      string input2 = ifr!;
      string[] result_ifr = input2.Split(':');

      string input3 = ifr_capota!;
      string[] result_ifr_capota = input3.Split(':');

      var hour_vfr = new TimeSpan(Convert.ToInt32(result_vfr[0]), Convert.ToInt32(result_vfr[1]), 0);
      var hour_ifr = new TimeSpan(Convert.ToInt32(result_ifr[0]), Convert.ToInt32(result_ifr[1]), 0);
      var hour_ifr_capota = new TimeSpan(Convert.ToInt32(result_ifr_capota[0]), Convert.ToInt32(result_ifr_capota[1]), 0);

      var total = hour_vfr + hour_ifr + hour_ifr_capota;

      return Convert.ToString(total);
    }
      
    }
}
