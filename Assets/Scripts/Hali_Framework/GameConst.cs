using UnityEngine;

namespace Hali_Framework
{
    public static class GameConst
    {
        //基础类型字节长度
        public const int INT_SIZE = sizeof(int);
        public const int LONG_SIZE = sizeof(long);
        public const int FLOAT_SIZE = sizeof(float);
        public const int BOOL_SIZE = sizeof(bool);
        
        //路径
        public static string DATA_BINARY_PATH = $"{Application.streamingAssetsPath}/Binary/";//Excel生成二进制数据文件夹
        
        //数据密钥
        public const byte KEY = 233;
        
        //太阳系数值
        //缩放倍率
        public const int ZOOM_RATIO = 10000;
        //万有引力常量
        public const float GRAVITATIONAL_CONSTANT = 667f;
        public const float TIME_STEP = 0.01f;
    }
}