using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;


public class PlayerPrefsX : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [XmlElement(ElementName = "Key")]
        public string _key;

        [XmlElement(ElementName = "StringValues")]
        public string[] Strings;

        [XmlElement(ElementName = "FloatValues")]
        public float[] Floats;

        [XmlElement(ElementName = "IntegerValues")]
        public int[] Integers;

        [XmlElement(ElementName = "BoolValues")]
        public bool[] Booleans;

        [XmlElement(ElementName = "LongValues")]
        public long[] Longs;

        public Data()
        {
        }

        public Data(string key)
        {
            _key = key;
        }

        #region Set Methods

        public bool SetInt(int v)
        {
            if (null != Integers && 1 == Integers.Length && v == Integers[0])
            {
                return false;
            }

            Integers = new int[1];
            Integers[0] = v;
            return true;
        }

        public bool SetIntArray(int[] v)
        {
            if (null != Integers && v.Length == Integers.Length)
            {
                bool diff = false;
                for (int i = 0; i < Integers.Length; i++)
                {
                    if (v[i] != Integers[i])
                    {
                        diff = true;
                        break;
                    }
                }

                //! 동일한 정보일 경우 즐
                if (false == diff)
                {
                    return false;
                }
            }

            Integers = v;
            return true;
        }

        public bool SetLong(long v)
        {
            if (null != Longs && 1 == Longs.Length && v == Longs[0])
            {
                return false;
            }
            Longs = new long[1];
            Longs[0] = v;
            
            return true;
        }

        public bool SetLongArray(long[] v)
        {
            if (null != Longs && v.Length == Longs.Length)
            {
                bool diff = false;
                for (int i = 0; i < Longs.Length; i++)
                {
                    if (v[i] != Longs[i])
                    {
                        diff = true;
                        break;
                    }
                }

                //! 동일한 정보일 경우 즐
                if (false == diff)
                {
                    return false;
                }
            }

            Longs = v;
            return true;
        }

        public bool SetFloat(float v)
        {
            if (null != Floats && 1 == Floats.Length && v == Floats[0])
            {
                return false;
            }

            Floats = new float[1];
            Floats[0] = v;
            return true;
        }

        public bool SetFloatArray(float[] v)
        {
            if (null != Floats && v.Length == Floats.Length)
            {
                bool diff = false;
                for (int i = 0; i < Floats.Length; i++)
                {
                    if (v[i] != Floats[i])
                    {
                        diff = true;
                        break;
                    }
                }

                //! 동일한 정보일 경우 즐
                if (false == diff)
                {
                    return false;
                }
            }

            Floats = v;
            return true;
        }

        public bool SetString(string v)
        {
            if (null != Strings && 1 == Strings.Length && v == Strings[0])
            {
                return false;
            }

            Strings = new string[1];
            Strings[0] = v;
            return true;
        }

        public bool SetStringArray(string[] v)
        {
            if (null != Strings && v.Length == Strings.Length)
            {
                bool diff = false;
                for (int i = 0; i < Strings.Length; i++)
                {
                    if (v[i] != Strings[i])
                    {
                        diff = true;
                        break;
                    }
                }

                //! 동일한 정보일 경우 즐
                if (false == diff)
                {
                    return false;
                }
            }

            Strings = v;
            return true;
        }

        public bool SetBool(bool v)
        {
            if (null != Booleans && 1 == Booleans.Length && v == Booleans[0])
            {
                return false;
            }

            Booleans = new bool[1];
            Booleans[0] = v;
            return true;
        }

        public bool SetBoolArray(bool[] v)
        {
            if (null != Booleans && v.Length == Booleans.Length)
            {
                bool diff = false;
                for (int i = 0; i < Booleans.Length; i++)
                {
                    if (v[i] != Booleans[i])
                    {
                        diff = true;
                        break;
                    }
                }

                //! 동일한 정보일 경우 즐
                if (false == diff)
                {
                    return false;
                }
            }

            Booleans = v;
            return true;
        }

        #endregion


        #region Get Methods

        public int GetInt(int defaultValue)
        {
            if (null == Integers || 0 == Integers.Length)
            {
                return defaultValue;
            }

            return Integers[0];
        }

        public int[] GetIntArray()
        {
            return Integers;
        }

        public long GetLong(long defaultValue)
        {
            if (null == Longs || 0 == Longs.Length)
            {
                return defaultValue;
            }

            return Longs[0];
        }

        public long[] GetLongArray()
        {
            return Longs;
        }
        

        public float GetFloat(float defaultValue)
        {
            if (null == Floats || 0 == Floats.Length)
            {
                return defaultValue;
            }

            return Floats[0];
        }

        public float[] GetFloatArray()
        {
            return Floats;
        }

        public string GetString(string defaultValue)
        {
            if (null == Strings || 0 == Strings.Length)
            {
                return defaultValue;
            }

            return Strings[0];
        }

        public string[] GetStringArray()
        {
            return Strings;
        }

        public bool GetBool(bool defaultValue)
        {
            if (null == Booleans || 0 == Booleans.Length)
            {
                return defaultValue;
            }

            return Booleans[0];
        }

        public bool[] GetBoolArray()
        {
            return Booleans;
        }
        #endregion
    }

    [System.Serializable]
    [XmlRoot("PlayerPrefsRoot")]
    public class DataRoot
    {
        [XmlArray("PlayerPrefsElements")]
        [XmlArrayItem("Data")]
        public List<Data> _data;
    }

    protected static PlayerPrefsX _instance = null;

    protected DataRoot _dataRoot = new DataRoot();
    protected Dictionary<string, Data> _dictionary = new Dictionary<string, Data>();

    public bool _changed = false;
    protected readonly string secretKey = "41424344";    //! must be 64 bits, 8 bytes
    protected float lastUpdatedTime;

    public static string errorMsg = string.Empty;

    public bool changed
    {
        get { return _changed; }
        set
        {
            if (true == _changed)
            {
                return;
            }

            _changed = value;
        }
    }

    public static PlayerPrefsX instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = Init();
            }

            return _instance;
        }
    }

    public static float intervalTime
    {
        get { return 0.0f; }
    }

    /// <summary>
    /// 암호화 여부를 체크합니다.
    /// 에디터일 경우 암호화를 하지 않습니다.
    /// </summary>
    protected bool needCrypt
    {
        get
        {
#if UNITY_EDITOR
            return false;
#else
            return true;
#endif
        }
    }

    public static string dataFileName
    {
        get
        {
#if UNITY_EDITOR
            return "savedata.xml";
#else
            return "jack.luv";
#endif
        }
    }

    public static string tempFileName
    {
        get
        {
#if UNITY_EDITOR
            return "savedata.tmp";
#else
            return "jack.tmp";
#endif
        }
    }

    /// <summary>
    /// 파일을 저장할 위치 정보입니다.
    /// 안드로이드의 경우 저장 위치가 달라지므로 주의하도록 합시다.
    /// </summary>
    public static string dirPath
    {
        get
        {
            return Application.persistentDataPath;
        }
    }

    static public string dataPathName
    {
        get { return Path.Combine(dirPath, dataFileName); }
    }

    public static string tempPathName
    {
        get { return Path.Combine(dirPath, tempFileName); }
    }

    static PlayerPrefsX Init()
    {
#if UNITY_EDITOR
        if (false == Application.isPlaying)
        {
            return null;
        }
#endif

        string name = "PlayerPrefsX";

        UnityEngine.Object find = UnityEngine.Object.FindObjectOfType(typeof(PlayerPrefsX));
        
        if (null != find)
        {
            return (PlayerPrefsX)find;
        }

        GameObject go = new GameObject(name, typeof(PlayerPrefsX));
        PlayerPrefsX pp = go.GetComponent<PlayerPrefsX>();
        GameObject.DontDestroyOnLoad(go);

        pp.Load();
        return pp;
    }

    // Update is called once per frame
    void Update()
    {
        if (changed && Time.time - lastUpdatedTime > intervalTime)
        //if (changed && Time.time > lastUpdatedTime)
        {
              Save();
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (changed && pause)
        {
            Save();
        }
    }

    void OnApplicationQuit()
    {
        if (changed)
        {
            Save();
        }
    }

    #region Save/Load
    public void Load()
    {
        object load = null;
        
        string filename = string.Empty;

        string oldfile1 = Application.persistentDataPath + "/never_touch_this";
        string oldfile2 = oldfile1;
        if (0 == PlayerPrefs.GetInt("Steak", 0))
            oldfile2 += ".bak";
        else
            oldfile1 += ".bak";


        string[] files = { dataPathName, tempPathName, oldfile1, oldfile2};
        foreach (string pathname in files)
        {
            load = LoadFile(pathname, needCrypt);
            if (null != load)
            {
                filename = pathname;
                break;
            }
        }

        if (null != load)
        {
            _dataRoot = (DataRoot)load;
            if (filename != dataPathName)
            {
                File.Delete(dataPathName);
                File.Move(filename, dataPathName);
            }
        }
        else
        {
            _dataRoot = new DataRoot();
            _dataRoot._data = new List<Data>();
        }
        
        lastUpdatedTime = Time.time;
        UpdateDictionary();
    }

    protected object LoadFile(string filePath, bool crypt)
    {
        try
        {
            object o = null;

            if (File.Exists(filePath) == false)
            {
                Debug.Log(System.String.Format("File({0}) does not exists", filePath));
                return null;
            }

            if (crypt)
            {
                DESCryptoServiceProvider mCryptoProvider = new DESCryptoServiceProvider();
                mCryptoProvider.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);
                mCryptoProvider.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);

                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    //Debug.Log("Opening...");

                    using (CryptoStream cs = new CryptoStream(fs, mCryptoProvider.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        //Debug.Log("CryptoStream");

                        using (StreamReader sr = new StreamReader(cs, System.Text.Encoding.UTF8))
                        {
                            //Debug.Log("StreamReader");

                            XmlSerializer s = new XmlSerializer(typeof(DataRoot));

                            //Debug.Log("Deserializing...");
                            o = s.Deserialize(sr);
                        }
                    }
                }
            }
            else
            {
                using (TextReader r = new StreamReader(filePath))
                {
                    XmlSerializer s = new XmlSerializer(typeof(DataRoot));
                    o = s.Deserialize(r);
                }
            }

            return o;
        }
        catch (System.Exception ex)
        {
            Debug.Log("Exception: " + filePath);
            Debug.LogWarning(ex.ToString());
        }

        return null;
    }

    static public void Reset()
    {
        if (File.Exists(dataPathName))
        {
            File.Delete(dataPathName);
        }

        if (File.Exists(tempPathName))
        {
            File.Delete(tempPathName);
        }

        DeleteAll();
    }

    /// <summary>
    /// 변경된 내용을 즉시 저장합니다.
    /// </summary>
    public static void SaveImmediately()
    {
        if (null == instance)
        {
            return;
        }

        instance.Save();
    }

    public void Save()
    {
        try
        {
            errorMsg = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    SaveSafely();
                    break;
                default:
                    SaveDirectly();
                    break;
            }

            _changed = false;
        }
        catch (System.Exception ex)
        {
            errorMsg = ex.ToString();
            Debug.LogWarning(ex.ToString());
        }
        finally
        {
            lastUpdatedTime = Time.time;
        }
    }


    void SaveDirectly()
    {
        using (FileStream fs = File.Open(tempPathName, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            if (needCrypt)
            {
                DESCryptoServiceProvider mCryptoProvider = new DESCryptoServiceProvider();
                mCryptoProvider.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);
                mCryptoProvider.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);

                using (CryptoStream cs = new CryptoStream(fs, mCryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs, System.Text.Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(DataRoot));
                        serializer.Serialize(sw, _dataRoot);
                    }
                }
            }
            else
            {
                XmlSerializer s = new XmlSerializer(typeof(DataRoot));
                s.Serialize(fs, _dataRoot);
            }
        }

        Rename();
    }

    /// <summary>
    /// 파일 저장 도중 디바이스가 종료될 경우 파일이 깨지는 걸 방지하기 위한 기능입니다.
    /// 임시 파일이 물리적인 위치에 저장이 완료 됐을 경우에만 실제 저장 파일로 이름을 변경합니다.
    /// </summary>
    void SaveSafely()
    {
        bool done = false;

        if (File.Exists(tempPathName))
        {
            File.Delete(tempPathName);
        }

        using (MemoryStream ms = new MemoryStream())
        {
            DESCryptoServiceProvider mCryptoProvider = new DESCryptoServiceProvider();
            mCryptoProvider.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);
            mCryptoProvider.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);

            using (CryptoStream cs = new CryptoStream(ms, mCryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs, System.Text.Encoding.UTF8))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    XmlSerializer serializer = new XmlSerializer(typeof(DataRoot));
                    serializer.Serialize(sw, _dataRoot, ns);

                    cs.FlushFinalBlock();

                    using (FileStream fs = File.Open(tempPathName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        ms.WriteTo(fs);
                        ms.Flush();
                        done = true;
                    }
                }
            }
        }

        if (done)
        {
            Rename();
        }

        //Debug.Log(string.Format("Save {0}: " + Time.time.ToString(), done ? "Success" : "Fail"));
    }

    /// <summary>
    /// 임시 파일을 저장 파일로 이름을 변경합니다.
    /// </summary>
    void Rename()
    {
        // 제대로 저장됐다면 임시 파일을 저장 파일로 변경해준다.
        File.Delete(dataPathName);
        File.Move(tempPathName, dataPathName);
     }
    #endregion


    #region Internal Methods
    public Data GetData(string key)
    {
        Data data = null;
        _dictionary.TryGetValue(key, out data);
        return data;
    }

    public Data NewData(string key)
    {
        if (_dictionary.ContainsKey(key))
        {
            return _dictionary[key];
        }

        Data data = new Data(key);
        
        _dataRoot._data.Add(data);
        _dictionary.Add(key, data);

        changed = true;

        return data;
    }

    public void RemoveData(string key)
    {
        Data data = GetData(key);
        if (null == data)
        {
            return;
        }

        _dataRoot._data.Remove(data);
        _dictionary.Remove(key);

        changed = true;
    }

    public void RemoveDataAll()
    {
        _dataRoot._data.Clear();
        _dictionary.Clear();
        changed = true;
    }


    void UpdateDictionary()
    {
        _dictionary.Clear();
        foreach (var v in _dataRoot._data)
        {
#if UNITY_EDITOR
            if (_dictionary.ContainsKey(v._key))
            {
                Debug.LogError("Already Exists : " + v._key);
            }
#endif
            _dictionary.Add(v._key, v);
        }
    }

    #endregion


    #region Get/Set Methods
    public static void SetInt(string key, int value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetInt(value);
    }

    public static void SetIntArray(string key, params int[] value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetIntArray(value);
    }

    public static void SetLong(string key, long value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetLong(value);
    }

    public static void SetLongArray(string key, params long[] value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetLongArray(value);
    }

    public static void SetFloat(string key, float value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetFloat(value);
    }

    public static void SetFloatArray(string key, params float[] value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetFloatArray(value);
    }

    public static void SetString(string key, string value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetString(value);
    }

    public static void SetStringArray(string key, params string[] value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetStringArray(value);
    }

    public static void SetBool(string key, bool value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetBool(value);
    }

    public static void SetBoolArray(string key, params bool[] value)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Data result = instance.GetData(key);
        if (null == result)
        {
            result = instance.NewData(key);
        }

        instance.changed = result.SetBoolArray(value);
    }



    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static long GetLong(string key)
    {
        return GetLong(key, 0);
    }


    public static float GetFloat(string key)
    {
        return GetFloat(key, 0f);
    }

    public static string GetString(string key)
    {
        return GetString(key, "");
    }

    public static bool GetBool(string key)
    {
        return GetBool(key, false);
    }

    public static int GetInt(string key, int defaultValue)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return defaultValue;
        }

        Data data = instance.GetData(key);
        if (null != data)
        {
            return data.GetInt(defaultValue);
        }

#if KEEP_OLD_DATA
        if (HasOldKey(key))
        {
            int value = PlayerPrefs.GetInt(key);
            SetInt(key, value);
            return value;
        }
#endif
        return defaultValue;
    }

    public static long GetLong(string key, long defaultValue)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return defaultValue;
        }

        Data data = instance.GetData(key);
        if (null != data)
        {
            return data.GetLong(defaultValue);
        }

#if KEEP_OLD_DATA
        if (HasOldKey(key))
        {
            int value = PlayerPrefs.GetInt(key);
            SetInt(key, value);
            return value;
        }
#endif
        return defaultValue;
    }


    public static float GetFloat(string key, float defaultValue)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return defaultValue;
        }

        Data data = instance.GetData(key);
        if (null != data)
        {
            return data.GetFloat(defaultValue);
        }

#if KEEP_OLD_DATA
        if (HasOldKey(key))
        {
            float value = PlayerPrefs.GetFloat(key);
            SetFloat(key, value);
            return value;
        }
#endif

        return defaultValue;
    }

    public static string GetString(string key, string defaultValue)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return defaultValue;
        }

        Data data = instance.GetData(key);
        if (null != data)
        {
            return data.GetString(defaultValue);
        }

#if KEEP_OLD_DATA
        if (HasOldKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            SetString(key, value);
            return value;
        }
#endif

        return defaultValue;

    }

    public static bool GetBool(string key, bool defaultValue)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return defaultValue;
        }

        Data data = instance.GetData(key);

        if (null != data)
        {
            return data.GetBool(defaultValue);
        }

#if KEEP_OLD_DATA
        if (HasOldKey(key))
        {
            bool value = (1 == PlayerPrefs.GetInt(key, defaultValue ? 1 : 0)) ? true : false;
            SetBool(key, value);
            return value;
        }
#endif

        return defaultValue;

    }


#if KEEP_OLD_DATA
    public static bool HasOldKey(string key)
    {
        return (PlayerPrefs.HasKey(key));
    }
#endif


    public static int[] GetIntArray(string key)
    {
        return GetIntArray(key, 0, 0);
    }

    public static int[] GetIntArray(string key, int defaultValue, int defaultSize)
    {
        int[] intArray = null;
        Data data = null;

        if (Application.isPlaying)
        {
            data = instance.GetData(key);
        }

        if (null != data)
        {
            intArray = data.GetIntArray();
        }
#if KEEP_OLD_DATA
        else if (HasOldKey(key))
        {
            string[] stringArray = PlayerPrefs.GetString(key).Split("|"[0]);
            intArray = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                intArray[i] = System.Convert.ToInt32(stringArray[i]);
            }
        }
#endif

        if (null == intArray)
        {
            intArray = new int[defaultSize];

            if (defaultSize > 0)
            {
                for (int i = 0; i < defaultSize; i++)
                {
                    intArray[i] = defaultValue;
                }
            }
        }

        return intArray;
    }



    public static long[] GetLongArray(string key)
    {
        return GetLongArray(key, 0, 0);
    }

    public static long[] GetLongArray(string key, long defaultValue, int defaultSize)
    {
        long[] longArray = null;
        Data data = null;

        if (Application.isPlaying)
        {
            data = instance.GetData(key);
        }

        if (null != data)
        {
            longArray = data.GetLongArray();
        }
#if KEEP_OLD_DATA
        else if (HasOldKey(key))
        {
            string[] stringArray = PlayerPrefs.GetString(key).Split("|"[0]);
            intArray = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                intArray[i] = System.Convert.ToInt32(stringArray[i]);
            }
        }
#endif

        if (null == longArray)
        {
            longArray = new long[defaultSize];

            if (defaultSize > 0)
            {
                for (int i = 0; i < defaultSize; i++)
                {
                    longArray[i] = defaultValue;
                }
            }
        }
        return longArray;
    }

    public static float[] GetFloatArray(string key)
    {
        return GetFloatArray(key, 0.0f, 0);
    }

    public static float[] GetFloatArray(string key, float defaultValue, int defaultSize)
    {
        float[] floatArray = null;
        Data data = null;

        if (Application.isPlaying)
        {
            data = instance.GetData(key);
        }

        if (null != data)
        {
            floatArray = data.GetFloatArray();
        }
#if KEEP_OLD_DATA
        else if (HasOldKey(key))
        {
            string[] stringArray = PlayerPrefs.GetString(key).Split("|"[0]);
            floatArray = new float[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                floatArray[i] = System.Convert.ToInt32(stringArray[i]);
            }
        }
#endif

        if (null == floatArray)
        {
            floatArray = new float[defaultSize];

            if (defaultSize > 0)
            {
                for (int i = 0; i < defaultSize; i++)
                    floatArray[i] = defaultValue;
            }
        }

        return floatArray;
    }

    public static string[] GetStringArray(string key)
    {
        return GetStringArray(key, "", 0);
    }

    public static string[] GetStringArray(string key, string defaultValue, int defaultSize)
    {
        string[] stringArray = null;
        Data data = null;

        if (Application.isPlaying)
        {
            data = instance.GetData(key);
        }

        if (null != data)
        {
            stringArray = data.GetStringArray();
        }

        if (null == stringArray)
        {
            stringArray = new string[defaultSize];
            for (int i = 0; i < defaultSize; i++)
                stringArray[i] = defaultValue;
        }

        return stringArray;
    }

    public static bool[] GetBoolArray(string key)
    {
        return GetBoolArray(key, false, 0);
    }

    public static bool[] GetBoolArray(string key, bool defaultValue, int defaultSize)
    {
        bool[] boolArray = null;
        Data data = null;

        if (Application.isPlaying)
        {
            data = instance.GetData(key);
        }

        if (null != data)
        {
            boolArray = data.GetBoolArray();
        }

        if (null == boolArray)
        {
            boolArray = new bool[defaultSize];
            for (int i = 0; i < defaultSize; i++)
                boolArray[i] = defaultValue;
        }

        return boolArray;
    }


    public static bool HasKey(string key)
    {
        if (Application.isPlaying)
        {
            return null != instance.GetData(key);
        }

        return false;
    }

    public static void DeleteKey(string key)
    {
        if (Application.isPlaying)
        {
            instance.RemoveData(key);
        }
    }

    public static void DeleteAll()
    {
        if (Application.isPlaying)
        {
            instance.RemoveDataAll();
        }
    }

    #endregion
}

