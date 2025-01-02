import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        Product nexus = new Product("Google", 123);
        Product iPhone = new Product( "Apple", 12345);
        Product huawei = new Product("Huawei", 423532);

        List<Product> products = Arrays.asList(nexus, iPhone, huawei);

        Collections.sort(products);

        System.out.println(products);
    }
}

class Product implements Comparable<Product> {
    private final String brandName;
    private final long modelNumber;

    public Product( String brandName, long modelNumber) {
        this.brandName = brandName;
        this.modelNumber = modelNumber;
    }

    @Override
    public int compareTo(Product other) {
        return other.brandName.compareTo(this.brandName);
    }

    @Override
    public String toString() {
        return ""+modelNumber;
    }
}