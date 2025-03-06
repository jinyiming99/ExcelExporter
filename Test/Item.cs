namespace Config
{
   public class ItemConfig
    {
       public int Id;
       public int Name;
       public int Handprop;
       public int Holdonetype;
       public int Holdstay;
       public int Regular;
       public int Handlock;
       public List<EdgeCustomAttr> Edge;
       public List<EdgeendCustomAttr> Edgeend;
       public int Opui;
       public int Op;
       public int Flitem;
       public List<int> Extend;
       public int Scaling;
       public RectifyCustomAttr Rectify;
       public PosCustomAttr Pos;
       public int Clue;
       public HandValueCustomAttr HandValue;
       public class EdgeCustomAttr
       {
          public int Id;
          public int State;
       }

       public class EdgeendCustomAttr
       {
          public int Id;
          public int State;
       }

       public class RectifyCustomAttr
       {
          public int X;
          public int Y;
          public int Z;
       }

       public class PosCustomAttr
       {
          public int X;
          public int Y;
          public int Z;
       }

       public class HandValueCustomAttr
       {
          public double Lg;
          public double Lt;
          public double Rg;
          public double Rt;
       }

}
}
