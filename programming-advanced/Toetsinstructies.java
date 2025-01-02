import java.util.ArrayList;

public class Toetsinstructies {
    public static void main(String[] args) {
        ArrayList<String> studenten = new ArrayList<>();

        studenten.add("Kenny");
        studenten.add("Marie");
        studenten.add("Nabil");
        studenten.add(1, "Nabil");

        System.out.println(studenten);
    }
}
