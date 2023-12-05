using System.Collections;

namespace UnityExplorer.Tests
{
    public class TestClass
    {
        static TestClass()
        {
            Init_Mono();
        }

        #region MONO

        public static object LiterallyAnything = null;

        public static string Exception
        {
            get
            {
                if (!shouldThrow)
                {
                    shouldThrow = true;
                    throw new Exception("This is a test.");
                }
                else
                {
                    shouldThrow = false;
                    return "No exception";
                }
            }
        }
        static bool shouldThrow;

        // Test enumerables
        public static int[,,] MultiDimensionalArray = new int[45, 45, 45];
        public static List<object> ListOfInts;
        public static List<List<List<string>>> NestedList;
        public static IDictionary MixedDictionary;
        public static Hashtable Hashtable;
        public static byte[] ByteArray = new byte[16];
        public static List<short> ABigList = new(10000);

        // Test const behaviour (should be a readonly field)
        public const int ConstantInt5 = 5;

        // Testing other InteractiveValues
        public static BindingFlags EnumTest;
        public static CameraClearFlags EnumTest2;
        public static Color Color = Color.magenta;
        public static Color32 Color32 = Color.red;
        public static string ALongString = new('#', 10000);

        public static float[] AParseTest(ref List<float[,,]> arg0, ref float[,] arg1)
        {
            return new float[] { 1, 2, 3 };
        }

        public static List<object> RandomList
        {
            get
            {
                List<object> list = new();
                int count = UnityEngine.Random.Range(0, 100);
                for (int i = 0; i < count; i++)
                    list.Add(GetRandomObject());
                return list;
            }
        }

        public int this[int index]
        {
            get => UnityEngine.Random.Range(0, int.MaxValue);
            set => ExplorerCore.Log(index);
        }

        // Test methods

        private static object GetRandomObject()
        {
            return UnityEngine.Random.Range(0, 7) switch
            {
                0 => null,
                1 => 123,
                2 => true,
                3 => "hello",
                4 => 50.5f,
                5 => CameraClearFlags.Color,
                6 => new List<string> { "one", "two" },
                _ => null,
            };
        }

        public static void TestComponent<T>() where T : Component
        {
            ExplorerCore.Log($"Test3 {typeof(T).FullName}");
        }

        public static void TestArgumentParse(string _string,
                                             int integer,
                                             Color color,
                                             CameraClearFlags flags,
                                             Vector3 vector,
                                             Quaternion quaternion,
                                             object obj,
                                             Type type,
                                             GameObject go)
        {
            ExplorerCore.Log($"_string: {_string}, integer: {integer}, color: {color.ToString()}, flags: {flags}, " +
                $"vector: {vector.ToString()}, quaternion: {quaternion.ToString()}, obj: {obj?.ToString() ?? "null"}," +
                $"type: {type?.FullName ?? "null"}, go: {go?.ToString() ?? "null"}");
        }

        private static void Init_Mono()
        {
            ExplorerCore.Log($"1: Basic list");
            ListOfInts = new List<object> { 1, 2, 3, 4, 5 };

            ExplorerCore.Log($"2: Nested list");
            NestedList = new List<List<List<string>>>
            {
                new List<List<string>> {
                    new List<string> { "1", "2", "3" },
                    new List<string> { "4", "5", "6" },
                },
                new List<List<string>>
                {
                    new List<string> { "7", "8", "9" }
                }
            };

            ExplorerCore.Log($"3: Dictionary");
            MixedDictionary = new Dictionary<object, object>
            {
                { 1, 2 },
                { "one", "two" },
                { true, false },
                { new Vector3(0,1,2), new Vector3(1,2,3) },
                { CameraClearFlags.Depth, CameraClearFlags.Color },
                { "################################################\r\n##########", null },
                { "subdict", new Dictionary<object,object> { { "key", "value" } } }
            };

            ExplorerCore.Log($"4: Hashtable");
            Hashtable = new Hashtable { { "One", 1 }, { "Two", 2 } };

            ExplorerCore.Log($"5: Big list");
            for (int i = 0; i < ABigList.Capacity; i++)
                ABigList.Add((short)UnityEngine.Random.Range(0, short.MaxValue));

            ExplorerCore.Log("Finished TestClass Init_Mono");
        }

        #endregion
    }
}
