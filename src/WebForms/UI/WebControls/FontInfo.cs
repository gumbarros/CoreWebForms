// MIT License.

using System.ComponentModel;
using System.Drawing;
using System.Globalization;

#nullable disable

namespace System.Web.UI.WebControls;
/// <devdoc>
///    <para>Represents the font properties for text. This class cannot be inherited.</para>
/// </devdoc>
[
    TypeConverterAttribute(typeof(ExpandableObjectConverter))
]
public sealed class FontInfo
{

    private readonly Style owner;

    /// <devdoc>
    /// </devdoc>
    internal FontInfo(Style owner)
    {
        this.owner = owner;
    }

    /// <devdoc>
    ///    <para>Indicates whether the text is bold.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(false),
        WebSysDescription(SR.FontInfo_Bold),
        NotifyParentProperty(true)
    ]
    public bool Bold
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_BOLD) ? (bool)owner.ViewState["Font_Bold"] : false;
        }
        set
        {
            owner.ViewState["Font_Bold"] = value;
            owner.SetBit(Style.PROP_FONT_BOLD);
        }
    }

    /// <devdoc>
    ///    <para>Indicates whether the text is italic.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(false),
        WebSysDescription(SR.FontInfo_Italic),
        NotifyParentProperty(true)
    ]
    public bool Italic
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_ITALIC) ? (bool)owner.ViewState["Font_Italic"] : false;
        }
        set
        {
            owner.ViewState["Font_Italic"] = value;
            owner.SetBit(Style.PROP_FONT_ITALIC);
        }
    }

    /// <devdoc>
    ///    <para>Indicates the name of the font.</para>
    /// </devdoc>
    [
        TypeConverterAttribute(typeof(FontConverter.FontNameConverter)),
        WebCategory("Appearance"),
        DefaultValue(""),
        WebSysDescription(SR.FontInfo_Name),
        NotifyParentProperty(true),
        RefreshProperties(RefreshProperties.Repaint),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
    ]
    public string Name
    {
        get
        {
            string[] names = Names;
            return names.Length > 0 ? names[0] : string.Empty;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length == 0)
            {
                Names = null;
            }
            else
            {
                Names = new string[1] { value };
            }
        }
    }

    /// <devdoc>
    /// </devdoc>
    [
        TypeConverterAttribute(typeof(FontNamesConverter)),
        WebCategory("Appearance"),
        WebSysDescription(SR.FontInfo_Names),
        RefreshProperties(RefreshProperties.Repaint),
        NotifyParentProperty(true)
    ]
    public string[] Names
    {
        get
        {
            if (owner.IsSet(Style.PROP_FONT_NAMES))
            {
                string[] names = (string[])owner.ViewState["Font_Names"];
                if (names != null)
                {
                    return names;
                }
            }
            return [];
        }
        set
        {
            owner.ViewState["Font_Names"] = value;
            owner.SetBit(Style.PROP_FONT_NAMES);
        }
    }

    /// <devdoc>
    ///    <para>Indicates whether the text is overline.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(false),
        WebSysDescription(SR.FontInfo_Overline),
        NotifyParentProperty(true)
    ]
    public bool Overline
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_OVERLINE) ? (bool)owner.ViewState["Font_Overline"] : false;
        }
        set
        {
            owner.ViewState["Font_Overline"] = value;
            owner.SetBit(Style.PROP_FONT_OVERLINE);
        }
    }

    /// <devdoc>
    /// </devdoc>
    internal Style Owner => owner;

    /// <devdoc>
    ///    <para>Indicates the font size.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(typeof(FontUnit), ""),
        WebSysDescription(SR.FontInfo_Size),
        NotifyParentProperty(true),
        RefreshProperties(RefreshProperties.Repaint)
    ]
    public FontUnit Size
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_SIZE) ? (FontUnit)owner.ViewState["Font_Size"] : FontUnit.Empty;
        }
        set
        {
            if ((value.Type == FontSize.AsUnit) && (value.Unit.Value < 0))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            owner.ViewState["Font_Size"] = value;
            owner.SetBit(Style.PROP_FONT_SIZE);
        }
    }

    /// <devdoc>
    ///    <para>Indicates whether the text is striked out.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(false),
        WebSysDescription(SR.FontInfo_Strikeout),
        NotifyParentProperty(true)
    ]
    public bool Strikeout
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_STRIKEOUT) ? (bool)owner.ViewState["Font_Strikeout"] : false;
        }
        set
        {
            owner.ViewState["Font_Strikeout"] = value;
            owner.SetBit(Style.PROP_FONT_STRIKEOUT);
        }
    }

    /// <devdoc>
    ///    <para>Indicates whether the text is underlined.</para>
    /// </devdoc>
    [
        WebCategory("Appearance"),
        DefaultValue(false),
        WebSysDescription(SR.FontInfo_Underline),
        NotifyParentProperty(true)
    ]
    public bool Underline
    {
        get
        {
            return owner.IsSet(Style.PROP_FONT_UNDERLINE) ? (bool)owner.ViewState["Font_Underline"] : false;
        }
        set
        {
            owner.ViewState["Font_Underline"] = value;
            owner.SetBit(Style.PROP_FONT_UNDERLINE);
        }
    }

    /// <devdoc>
    /// <para>Resets all properties that have their default value to their unset state. </para>
    /// </devdoc>
    public void ClearDefaults()
    {
        if (Names.Length == 0)
        {
            owner.ViewState.Remove("Font_Names");
            owner.ClearBit(Style.PROP_FONT_NAMES);
        }
        if (Size == FontUnit.Empty)
        {
            owner.ViewState.Remove("Font_Size");
            owner.ClearBit(Style.PROP_FONT_SIZE);
        }
        if (Bold == false)
        {
            ResetBold();
        }

        if (Italic == false)
        {
            ResetItalic();
        }

        if (Underline == false)
        {
            ResetUnderline();
        }

        if (Overline == false)
        {
            ResetOverline();
        }

        if (Strikeout == false)
        {
            ResetStrikeout();
        }
    }

    /// <devdoc>
    /// <para>Copies the font properties of another <see cref='System.Web.UI.WebControls.FontInfo'/> into this instance. </para>
    /// </devdoc>
    public void CopyFrom(FontInfo f)
    {
        if (f != null)
        {
            Style fOwner = f.Owner;
            if (fOwner.RegisteredCssClass.Length != 0)
            {
                if (fOwner.IsSet(Style.PROP_FONT_NAMES))
                {
                    ResetNames();
                }

                if (fOwner.IsSet(Style.PROP_FONT_SIZE) && (f.Size != FontUnit.Empty))
                {
                    ResetFontSize();
                }

                if (fOwner.IsSet(Style.PROP_FONT_BOLD))
                {
                    ResetBold();
                }

                if (fOwner.IsSet(Style.PROP_FONT_ITALIC))
                {
                    ResetItalic();
                }

                if (fOwner.IsSet(Style.PROP_FONT_OVERLINE))
                {
                    ResetOverline();
                }

                if (fOwner.IsSet(Style.PROP_FONT_STRIKEOUT))
                {
                    ResetStrikeout();
                }

                if (fOwner.IsSet(Style.PROP_FONT_UNDERLINE))
                {
                    ResetUnderline();
                }
            }
            else
            {
                if (fOwner.IsSet(Style.PROP_FONT_NAMES))
                {
                    Names = f.Names;
                }
                if (fOwner.IsSet(Style.PROP_FONT_SIZE) && (f.Size != FontUnit.Empty))
                {
                    Size = f.Size;
                }

                // Only carry through true boolean values. Otherwise merging and copying
                // can do 3 different things for each property, but they are only persisted
                // as 2 state values.
                if (fOwner.IsSet(Style.PROP_FONT_BOLD))
                {
                    Bold = f.Bold;
                }

                if (fOwner.IsSet(Style.PROP_FONT_ITALIC))
                {
                    Italic = f.Italic;
                }

                if (fOwner.IsSet(Style.PROP_FONT_OVERLINE))
                {
                    Overline = f.Overline;
                }

                if (fOwner.IsSet(Style.PROP_FONT_STRIKEOUT))
                {
                    Strikeout = f.Strikeout;
                }

                if (fOwner.IsSet(Style.PROP_FONT_UNDERLINE))
                {
                    Underline = f.Underline;
                }
            }
        }
    }

    /// <devdoc>
    /// <para>Combines the font properties of another <see cref='System.Web.UI.WebControls.FontInfo'/> with this
    ///    instance. </para>
    /// </devdoc>
    public void MergeWith(FontInfo f)
    {
        if (f != null)
        {
            Style fOwner = f.Owner;
            if (fOwner.RegisteredCssClass.Length == 0)
            {
                if (fOwner.IsSet(Style.PROP_FONT_NAMES) && !owner.IsSet(Style.PROP_FONT_NAMES))
                {
                    Names = f.Names;
                }

                if (fOwner.IsSet(Style.PROP_FONT_SIZE) && (!owner.IsSet(Style.PROP_FONT_SIZE) || (Size == FontUnit.Empty)))
                {
                    Size = f.Size;
                }

                if (fOwner.IsSet(Style.PROP_FONT_BOLD) && !owner.IsSet(Style.PROP_FONT_BOLD))
                {
                    Bold = f.Bold;
                }

                if (fOwner.IsSet(Style.PROP_FONT_ITALIC) && !owner.IsSet(Style.PROP_FONT_ITALIC))
                {
                    Italic = f.Italic;
                }

                if (fOwner.IsSet(Style.PROP_FONT_OVERLINE) && !owner.IsSet(Style.PROP_FONT_OVERLINE))
                {
                    Overline = f.Overline;
                }

                if (fOwner.IsSet(Style.PROP_FONT_STRIKEOUT) && !owner.IsSet(Style.PROP_FONT_STRIKEOUT))
                {
                    Strikeout = f.Strikeout;
                }

                if (fOwner.IsSet(Style.PROP_FONT_UNDERLINE) && !owner.IsSet(Style.PROP_FONT_UNDERLINE))
                {
                    Underline = f.Underline;
                }
            }
        }
    }

    /// <devdoc>
    /// Resets all properties to their defaults.
    /// </devdoc>
    internal void Reset()
    {
        if (owner.IsSet(Style.PROP_FONT_NAMES))
        {
            ResetNames();
        }

        if (owner.IsSet(Style.PROP_FONT_SIZE))
        {
            ResetFontSize();
        }

        if (owner.IsSet(Style.PROP_FONT_BOLD))
        {
            ResetBold();
        }

        if (owner.IsSet(Style.PROP_FONT_ITALIC))
        {
            ResetItalic();
        }

        if (owner.IsSet(Style.PROP_FONT_UNDERLINE))
        {
            ResetUnderline();
        }

        if (owner.IsSet(Style.PROP_FONT_OVERLINE))
        {
            ResetOverline();
        }

        if (owner.IsSet(Style.PROP_FONT_STRIKEOUT))
        {
            ResetStrikeout();
        }
    }

    /// <devdoc>
    /// Only serialize if the Bold property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private void ResetBold()
    {
        owner.ViewState.Remove("Font_Bold");
        owner.ClearBit(Style.PROP_FONT_BOLD);
    }

    private void ResetNames()
    {
        owner.ViewState.Remove("Font_Names");
        owner.ClearBit(Style.PROP_FONT_NAMES);
    }

    private void ResetFontSize()
    {
        owner.ViewState.Remove("Font_Size");
        owner.ClearBit(Style.PROP_FONT_SIZE);
    }

    /// <devdoc>
    /// Only serialize if the Italic property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private void ResetItalic()
    {
        owner.ViewState.Remove("Font_Italic");
        owner.ClearBit(Style.PROP_FONT_ITALIC);
    }

    /// <devdoc>
    /// Only serialize if the Overline property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private void ResetOverline()
    {
        owner.ViewState.Remove("Font_Overline");
        owner.ClearBit(Style.PROP_FONT_OVERLINE);
    }

    /// <devdoc>
    /// Only serialize if the Strikeout property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private void ResetStrikeout()
    {
        owner.ViewState.Remove("Font_Strikeout");
        owner.ClearBit(Style.PROP_FONT_STRIKEOUT);
    }

    /// <devdoc>
    /// Only serialize if the Underline property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private void ResetUnderline()
    {
        owner.ViewState.Remove("Font_Underline");
        owner.ClearBit(Style.PROP_FONT_UNDERLINE);
    }

    /// <devdoc>
    /// Only serialize if the Bold property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private bool ShouldSerializeBold()
    {
        return owner.IsSet(Style.PROP_FONT_BOLD);
    }

    /// <devdoc>
    /// Only serialize if the Italic property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private bool ShouldSerializeItalic()
    {
        return owner.IsSet(Style.PROP_FONT_ITALIC);
    }

    /// <devdoc>
    /// Only serialize if the Overline property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private bool ShouldSerializeOverline()
    {
        return owner.IsSet(Style.PROP_FONT_OVERLINE);
    }

    /// <devdoc>
    /// Only serialize if the Strikeout property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private bool ShouldSerializeStrikeout()
    {
        return owner.IsSet(Style.PROP_FONT_STRIKEOUT);
    }

    /// <devdoc>
    /// Only serialize if the Underline property has changed.  This means that we serialize "false"
    /// if they were set to false in the designer.
    /// </devdoc>
    private bool ShouldSerializeUnderline()
    {
        return owner.IsSet(Style.PROP_FONT_UNDERLINE);
    }

    /// <internalonly/>
    public bool ShouldSerializeNames()
    {
        string[] names = Names;
        return names.Length > 0;
    }

    /// <devdoc>
    /// </devdoc>
    public override string ToString()
    {
        string size = this.Size.ToString(CultureInfo.InvariantCulture);
        string s = this.Name;

        if (size.Length != 0)
        {
            if (s.Length != 0)
            {
                s += ", " + size;
            }
            else
            {
                s = size;
            }
        }
        return s;
    }
}

