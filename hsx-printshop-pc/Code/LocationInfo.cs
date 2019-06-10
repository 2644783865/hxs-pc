using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace MaSoft.Code
{
    public class LocationInfo
    {
        private readonly string string_0;

        private readonly string string_1;

        private readonly string string_2;

        private readonly string string_3;

        private readonly string string_4;

        public string ClassName
        {
            get
            {
                return string_0;
            }
        }

        public string FileName
        {
            get
            {
                return string_1;
            }
        }

        public string LineNumber
        {
            get
            {
                return string_2;
            }
        }

        public string MethodName
        {
            get
            {
                return string_3;
            }
        }

        public string FullInfo
        {
            get
            {
                return string_4;
            }
        }

        public LocationInfo(Type callerStackBoundaryDeclaringType)
        {


            string_0 = "?";
            string_1 = "?";
            string_2 = "?";
            string_3 = "?";
            string_4 = "?";
            if (callerStackBoundaryDeclaringType != null)
            {
                try
                {
                    StackTrace stackTrace = new StackTrace(true);
                    int i;
                    for (i = 0; i < stackTrace.FrameCount; i++)
                    {
                        StackFrame frame = stackTrace.GetFrame(i);
                        if (frame != null && frame.GetMethod().DeclaringType == callerStackBoundaryDeclaringType)
                        {
                            break;
                        }
                    }
                    for (; i < stackTrace.FrameCount; i++)
                    {
                        StackFrame frame2 = stackTrace.GetFrame(i);
                        if (frame2 != null && frame2.GetMethod().DeclaringType != callerStackBoundaryDeclaringType)
                        {
                            break;
                        }
                    }
                    if (i < stackTrace.FrameCount)
                    {
                        StackFrame frame3 = stackTrace.GetFrame(i);
                        if (frame3 != null)
                        {
                            MethodBase method = frame3.GetMethod();
                            if (method != null)
                            {
                                string_3 = method.Name;
                                if (method.DeclaringType != null)
                                {
                                    string_0 = method.DeclaringType.FullName;
                                }
                            }
                            string_1 = frame3.GetFileName();
                            string_2 = frame3.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
                            string_4 = string_0 + "." + string_3 + "(" + string_1 + ":" + string_2 + ")";
                        }
                    }
                }
                catch (SecurityException)
                {
                }
            }
        }

        public LocationInfo(string className, string methodName, string fileName, string lineNumber)
        {


            string_0 = className;
            string_1 = fileName;
            string_2 = lineNumber;
            string_3 = methodName;
            string_4 = string_0 + "." + string_3 + "(" + string_1 + ":" + string_2 + ")";
        }
    }
}
