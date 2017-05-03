// File generated by hadoop record compiler. Do not edit.
/**
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Linq;
using Org.Apache.Jute;
using Common.Logging;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetChildrenResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetChildrenResponse));
        public GetChildrenResponse()
        {
        }
        public GetChildrenResponse(
        System.Collections.Generic.IEnumerable<string> children
      )
        {
            Children = children;
        }
        public System.Collections.Generic.IEnumerable<string> Children { get; set; }
        public void Serialize(IOutputArchive a_, String tag)
        {
            a_.StartRecord(this, tag);
            {
                a_.StartVector(Children, "children");
                if (Children != null)
                {
                    foreach (var e1 in Children)
                    {
                        a_.WriteString(e1, e1);
                    }
                }
                a_.EndVector(Children, "children");
            }
            a_.EndRecord(this, tag);
        }
        public void Deserialize(IInputArchive a_, String tag)
        {
            a_.StartRecord(tag);
            {
                IIndex vidx1 = a_.StartVector("children");
                if (vidx1 != null)
                {
                    var tmpLst = new System.Collections.Generic.List<string>();
                    for (; !vidx1.Done(); vidx1.Incr())
                    {
                        String e1;
                        e1 = a_.ReadString("e1");
                        tmpLst.Add(e1);
                    }
                    Children = tmpLst;
                }
                a_.EndVector("children");
            }
            a_.EndRecord(tag);
        }
        public override String ToString()
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (ZooKeeperNet.IO.EndianBinaryWriter writer =
                  new ZooKeeperNet.IO.EndianBinaryWriter(ZooKeeperNet.IO.EndianBitConverter.Big, ms, System.Text.Encoding.UTF8))
                {
                    BinaryOutputArchive a_ =
                      new BinaryOutputArchive(writer);
                    a_.StartRecord(this, string.Empty);
                    {
                        a_.StartVector(Children, "children");
                        if (Children != null)
                        {
                            foreach (var e1 in Children)
                            {
                                a_.WriteString(e1, e1);
                            }
                        }
                        a_.EndVector(Children, "children");
                    }
                    a_.EndRecord(this, string.Empty);
                    ms.Position = 0;
                    return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return "ERROR";
        }
        public void Write(ZooKeeperNet.IO.EndianBinaryWriter writer)
        {
            BinaryOutputArchive archive = new BinaryOutputArchive(writer);
            Serialize(archive, string.Empty);
        }
        public void ReadFields(ZooKeeperNet.IO.EndianBinaryReader reader)
        {
            BinaryInputArchive archive = new BinaryInputArchive(reader);
            Deserialize(archive, string.Empty);
        }
        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing GetChildrenResponse is unimplemented");
        }
        public override bool Equals(object obj)
        {
            GetChildrenResponse peer = (GetChildrenResponse)obj;
            if (peer == null)
            {
                return false;
            }
            if (Object.ReferenceEquals(peer, this))
            {
                return true;
            }
            bool ret = false;
            ret = Children.Equals(peer.Children);
            if (!ret) return ret;
            return ret;
        }
        public override int GetHashCode()
        {
            int result = 17;
            int ret = GetType().GetHashCode();
            result = 37 * result + ret;
            ret = Children.GetHashCode();
            result = 37 * result + ret;
            return result;
        }
        public static string Signature()
        {
            return "LGetChildrenResponse([s])";
        }
    }
}
