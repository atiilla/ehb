
// Inheritance is a mechanism in which a new class is created using the properties of an existing class.
public class Program {
    public static void main(String[] args) {
        System.out.println("Test");
        Grandfather grandfather = new Grandfather(); // Grandfather object
        grandfather.print("");

        // Inherit the properties of the Grandfather class
        Father father = new Father(); // Father object
        father.print("-> Father");

        // Inherit the properties of the Grandfather class
        Son son = new Son(); // Son object
        son.print("-> Son");
    }
}

class Grandfather {
    public void print(String extraStr) {
        String str = extraStr != null? extraStr : "";
        System.out.println("Greetings from Grandfather " + str);
    }
}

class Father extends Grandfather {
    
}

class Son extends Father {
   
}

