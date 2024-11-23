namespace Hackacont2024.Models {

    // Represents the 'User' entity to be stored in the database.
    public class User {
        // Unique identifier for the user.
        public int Id { get; set; }
        
        // Name of the user.
        public string Name { get; set; }

        // A unique code assigned to each user.
        public int Codigo { get; set; }

        // The password for the user.
        public string Senha { get; set; }
    }

}
