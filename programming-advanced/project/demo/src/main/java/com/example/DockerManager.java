package com.example;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.concurrent.CompletableFuture;

public class DockerManager {
    public static void startService(String service) {
        try {
            String command = service.equals("all") ? "docker-compose up -d" : "docker-compose up -d " + service;
            System.out.println("Starting service: " + service);
            Process process = new ProcessBuilder("bash", "-c", command).start();
            process.waitFor();
            if (service.equals("all")) {
                System.out.println("All services started successfully.");
            } else {
                inspectService(service);
            }
        } catch (Exception e) {
            System.err.println("Error starting service '" + service + "': " + e.getMessage());
        }
    }

    public static void inspectService(String service) {
        try {
            String command = "docker inspect " + service;
            Process process = new ProcessBuilder("bash", "-c", command).start();
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));

            StringBuilder output = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null) {
                output.append(line);
            }
            process.waitFor();

            ServiceInfo info = parseDockerInspect(output.toString());
            System.out.println("Service Info:\n" + info);
        } catch (Exception e) {
            System.err.println("Error inspecting service '" + service + "': " + e.getMessage());
        }
    }

    private static ServiceInfo parseDockerInspect(String output) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        JsonNode root = mapper.readTree(output);

        JsonNode networks = root.get(0).get("NetworkSettings").get("Networks");
        JsonNode network = networks.get("vt_vulnerabilitytargets_default");
        String ipAddress = network.get("IPAddress").asText();

        JsonNode ports = root.get(0).get("NetworkSettings").get("Ports").get("80/tcp");
        String webPort = ports.get(0).get("HostPort").asText();

        return new ServiceInfo(ipAddress, webPort);
    }
}
