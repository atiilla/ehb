package com.example;
import java.util.Map;
import java.util.Map.Entry;

public class ServiceManager {
    static final Map<String, String[]> services = Map.of(
            "juice-shop", new String[]{"owasp", "api", "xss", "top10"},
            "dvwa", new String[]{"sql", "xss", "csrf", "php"},
            "mutillidae", new String[]{"xss", "csrf", "php", "owasp"},
            "bwapp", new String[]{"sql", "xss", "top10", "buggy"},
            "nodegoat", new String[]{"nodejs", "api", "nosql", "owasp"},
            "metasploitable", new String[]{"network", "os", "services"},
            "broken-crystals", new String[]{"api", "dotnet", "top10"},
            "dvws", new String[]{"api", "rest", "xss"}
    );

    public static void searchService(String keyword) {
        System.out.println("Searching for targets matching '" + keyword + "':");
        boolean found = false;

        for (Entry<String, String[]> entry : services.entrySet()) {
            String service = entry.getKey();
            String[] tags = entry.getValue();
            if (service.contains(keyword) || contains(tags, keyword)) {
                System.out.println(" # " + service + " (tags: " + String.join(", ", tags) + ")");
                found = true;
            }
        }

        if (!found) {
            System.err.println("No target found matching '" + keyword + "'.");
        }
    }

    private static boolean contains(String[] tags, String keyword) {
        for (String tag : tags) {
            if (tag.contains(keyword)) {
                return true;
            }
        }
        return false;
    }
}
