using System.Collections.Generic;
public class CelestialBodyInfoContainer : BaseContainer
{
   public Dictionary<int, CelestialBodyInfo> dataDic = new Dictionary<int, CelestialBodyInfo>();
    public override object GetDic() => dataDic;
}