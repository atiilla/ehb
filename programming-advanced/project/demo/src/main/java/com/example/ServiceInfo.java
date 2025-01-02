package com.example;
public class ServiceInfo {
    private final String ipAddress;
    private final String webPort;

    public ServiceInfo(String ipAddress, String webPort) {
        this.ipAddress = ipAddress;
        this.webPort = webPort != null ? webPort : "N/A";
    }

    @Override
    public String toString() {
        return String.format("- IP Address: %s\n- Web Port: %s", ipAddress, webPort);
    }
}
