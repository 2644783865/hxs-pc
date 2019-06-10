using System;
using System.ComponentModel;

namespace MaSoft.Code
{
    public class BackgroundTask
    {
        public static void BackgroundWork(Action<object> action, object obj)
        {
            using (var bw = new BackgroundWorker())
            {
                bw.DoWork += (s, e) =>
                {
                    try
                    {
                        Action<object> a = action;
                        a.Invoke(obj);
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception);
                    }
                };

                bw.RunWorkerAsync();
            }
        }
    }
}
