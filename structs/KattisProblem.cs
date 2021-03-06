using System;
using System.Collections.Generic;
using System.Text;

namespace KattisTableGenerator {
    public class KattisProblem : IComparable<KattisProblem>, IEquatable<KattisProblem> {
        private string name,
        id,
        url;
        private SortedSet<HyperLink> attachments;

        public KattisProblem (string name, string id) {
            this.name = name;
            this.id = id;
            url = $"https://open.kattis.com/problems/{id}";
            attachments = new SortedSet<HyperLink> ();
        }

        public KattisProblem (string id) : this (null, id) { }

        public bool Contains (string lang) {
            return attachments.Contains (new HyperLink (lang, null));
        }

        public void Add (string lang, string url) {
            attachments.Add (new HyperLink (lang, url));
        }

        public int CompareTo (KattisProblem other) {
            return name.Equals (other.name) ? id.CompareTo (other.id) : name.CompareTo (other.name);
        }

        public bool Equals (KattisProblem other) {
            return id.Equals (other.id);
        }

        public override bool Equals (object obj) {
            if (obj == null)
                return false;
            KattisProblem problem = obj as KattisProblem;
            if (problem == null)
                return false;
            return Equals (problem);
        }

        public override int GetHashCode () {
            return id.GetHashCode ();
        }

        public static bool operator == (KattisProblem problem1, KattisProblem problem2) {
            if ((object) problem1 == null || (object) problem2 == null)
                return Object.Equals (problem1, problem2);
            return problem1.Equals (problem2);
        }

        public static bool operator != (KattisProblem problem1, KattisProblem problem2) {
            if ((object) problem1 == null || (object) problem2 == null)
                return !Object.Equals (problem1, problem2);
            return !problem1.Equals (problem2);
        }

        public override string ToString () {
            int DEFAULT_SIZE = 1000;
            StringBuilder builder = new StringBuilder (DEFAULT_SIZE).Append ($"| [{name}]({url}) | ");
            bool first = true;
            foreach (HyperLink hyperlink in attachments) {
                if (!first)
                    builder.Append (", ");
                builder.Append (hyperlink);
                first = false;
            }
            builder.Append (" |");
            return builder.ToString ();
        }

        private class HyperLink : IComparable<HyperLink>,
        IEquatable<HyperLink> {
            public string Language { get; }
            public string Url { get; }

            public HyperLink (string lang, string url) {
                this.Language = lang;
                this.Url = url;
            }

            public HyperLink (string lang) : this (lang, null) { }

            public int CompareTo (HyperLink other) {
                return Language.CompareTo (other.Language);
            }

            public bool Equals (HyperLink other) {
                return Language.Equals (other.Language);
            }

            public override bool Equals (object obj) {
                if (obj == null)
                    return false;
                HyperLink hyperlink = obj as HyperLink;
                if (hyperlink == null)
                    return false;
                return Equals (hyperlink);
            }

            public override int GetHashCode () {
                return Language.GetHashCode ();
            }

            public static bool operator == (HyperLink hyperlink1, HyperLink hyperlink2) {
                if ((object) hyperlink1 == null || (object) hyperlink2 == null)
                    return Object.Equals (hyperlink1, hyperlink2);
                return hyperlink1.Equals (hyperlink2);
            }

            public static bool operator != (HyperLink hyperlink1, HyperLink hyperlink2) {
                if ((object) hyperlink1 == null || (object) hyperlink2 == null)
                    return !Object.Equals (hyperlink1, hyperlink2);
                return !hyperlink1.Equals (hyperlink2);
            }

            public override string ToString () {
                return $"[{Language}]({Url})";
            }
        }
    }
}