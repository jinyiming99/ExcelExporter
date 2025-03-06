namespace Config
{
   public class TaskConfig
    {
       public int Id;
       public int Guide;
       public List<int> Qianzhi_task;
       public List<Qianzhi_itemCustomAttr> Qianzhi_item;
       public List<TaskCustomAttr> Task;
       public List<int> Get;
       public List<Reward_itemCustomAttr> Reward_item;
       public List<Timer_itemCustomAttr> Timer_item;
       public List<int> Del;
       public List<int> Reward_task;
       public List<Reward_item_handpropCustomAttr> Reward_item_handprop;
       public class Qianzhi_itemCustomAttr
       {
          public int Id;
          public int State;
       }

       public class TaskCustomAttr
       {
          public int Id;
          public int State;
       }

       public class Reward_itemCustomAttr
       {
          public int Id;
          public int State;
          public int Delay;
       }

       public class Timer_itemCustomAttr
       {
          public int Id;
          public int State;
          public int Delay;
       }

       public class Reward_item_handpropCustomAttr
       {
          public int Id;
          public int Handprop;
       }

}
}
