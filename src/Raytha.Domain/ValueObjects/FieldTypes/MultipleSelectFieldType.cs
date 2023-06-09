﻿using Raytha.Domain.ValueObjects.FieldTypes;
using Raytha.Domain.ValueObjects.FieldValues;

public class MultipleSelectFieldType : BaseFieldType
{
    public MultipleSelectFieldType() : base("Multiple select", "multiple_select", true) { }

    public override IEnumerable<ConditionOperator> SupportedConditionOperators
    {
        get
        {
            yield return ConditionOperator.HAS;
            yield return ConditionOperator.NOT_HAS;
            yield return ConditionOperator.IS_EMPTY;
            yield return ConditionOperator.IS_NOT_EMPTY;
        }
    }

    public override BaseFieldValue FieldValueFrom(dynamic value)
    {
        return new ArrayFieldValue(value);
    }
}