﻿using System;
using SharpYaml;
using SharpYaml.Events;
using SharpYaml.Serialization;
using SiliconStudio.Core;
using SiliconStudio.Core.Yaml;

namespace SiliconStudio.Assets.Serializers
{
    [YamlSerializerFactory]
    public sealed class IdentifiableAssetPartReferenceSerializer : ScalarOrObjectSerializer
    {
        public override bool CanVisit(Type type)
        {
            return type == typeof(IdentifiableAssetPartReference);
        }

        public override object ConvertFrom(ref ObjectContext context, Scalar fromScalar)
        {
            Guid guid;
            if (!Guid.TryParse(fromScalar.Value, out guid))
            {
                throw new YamlException(fromScalar.Start, fromScalar.End, "Unable to decode asset part reference [{0}]. Expecting an ENTITY_GUID".ToFormat(fromScalar.Value));
            }

            var result = context.Instance as IdentifiableAssetPartReference ?? (IdentifiableAssetPartReference)(context.Instance = new IdentifiableAssetPartReference());
            result.Id = guid;

            return result;
        }

        public override string ConvertTo(ref ObjectContext objectContext)
        {
            return ((IdentifiableAssetPartReference)objectContext.Instance).Id.ToString();
        }
    }
}