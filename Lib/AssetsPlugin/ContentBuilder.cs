﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.AssetsPlugin
{
    public abstract class ContentBuilder
    {
        protected const string Notice = "//This is AUTOGENERATED FILE by bb-assets-generator-plugin. DO NOT EDIT!!!\n";
        const string ExportConst = "export const ";

        protected StringBuilder? ContentStringBuilder;

        public void Build(IDictionary<string, object> assets)
        {
            ContentStringBuilder = new StringBuilder();
            ContentStringBuilder.Append(GetHeader());
            ContentStringBuilder.Append('\n');
            RecursiveBuild(assets, 0);
        }

        public string Content => ContentStringBuilder!.ToString();

        protected abstract string GetHeader();

        protected virtual bool ShouldSkip(string value) => false;

        protected abstract void AddPropertyValue(string value);

        static string GetPropertyLineEnd(int depth)
        {
            return depth == 0 ? ";\n" : ",\n";
        }

        static string GetPropertyNameValueSeparator(int depth)
        {
            return depth == 0 ? " = " : ": ";
        }

        static string SanitizePropertyName(string key)
        {
            if (key == "") return "";
            if (key[0] >= '0' && key[0] <= '9') return "_" + key;
            return key;
        }

        void RecursiveBuild(IDictionary<string, object> rootObject, int depth)
        {
            foreach (var propertyPair in rootObject.OrderBy(p=>p.Key))
            {
                var propertyName = propertyPair.Key;
                var propertyValue = propertyPair.Value;
                if (propertyValue is string propertyString)
                {
                    if (ShouldSkip(propertyString)) continue;
                }
                if (depth == 0)
                {
                    AddExport();
                }
                else
                {
                    AddIdent(depth);
                }

                if (propertyValue is IDictionary<string, object> dictionary)
                {
                    AddObjectStart(propertyName, depth);
                    RecursiveBuild(dictionary, depth + 1);
                    AddObjectEnd(depth);
                }
                else
                {
                    ContentStringBuilder!.Append(SanitizePropertyName(propertyName));
                    ContentStringBuilder.Append(GetPropertyNameValueSeparator(depth));
                    AddPropertyValue((string)propertyValue);
                    ContentStringBuilder.Append(GetPropertyLineEnd(depth));
                }
            }
        }

        void AddObjectStart(string name, int depth)
        {
            ContentStringBuilder!.Append(SanitizePropertyName(name));
            ContentStringBuilder.Append(GetPropertyNameValueSeparator(depth));
            ContentStringBuilder.Append("{\n");
        }

        void AddObjectEnd(int depth)
        {
            AddIdent(depth);
            ContentStringBuilder!.Append('}');
            ContentStringBuilder.Append(depth == 0 ? ';' : ',');
            ContentStringBuilder.Append('\n');
        }

        void AddIdent(int depth)
        {
            ContentStringBuilder!.Append(' ', depth * 4);
        }

        void AddExport()
        {
            ContentStringBuilder!.Append(ExportConst);
        }
    }
}
