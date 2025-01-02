import java.util.TreeSet;

public class TreeSetEx {
    public static void main(String[] args) {
        TreeSet<String> vakken = new TreeSet<>(10);
        vakken.add("Programming essentials 2");
        vakken.add("Programming essentials 2"); 
        vakken.add("Static web");
        vakken.add("Dynamic web")
        vakken.remove("Programming essentials 2");

        System.out.println(vakken);

    }
}
