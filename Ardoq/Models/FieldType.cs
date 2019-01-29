namespace Ardoq.Models
{
    public enum FieldType
    {
        /*
         * Newtonsoft's json parser does not support EnumMember custom names
         */
        //@SerializedName("Text")
        Text,
        //@SerializedName("Textarea")
        Textarea,
        //@SerializedName("DateTime")
        DateTime,
        //@SerializedName("Checkbox")
        Checkbox,
        //@SerializedName("Number")
        Number,
        //@SerializedName("List")
        List,
        //@SerializedName("url")
        url,
        //@SerializedName("email")
        Email,
        //@SerializedName("SelectMultipleList")
        SelectMultipleList
    }
}