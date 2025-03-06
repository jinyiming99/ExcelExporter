namespace Config
{
   public class GameConfig
    {
       public static ItemConfigData itemConfigData = null;
       public static TaskConfigData taskConfigData = null;
       public static void Init()
       {
           itemConfigData = new ItemConfigData();
           taskConfigData = new TaskConfigData();
       }
    }
}
