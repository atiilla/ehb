package com.example;
import picocli.CommandLine;
import java.util.concurrent.Callable;

@CommandLine.Command(name = "vt", mixinStandardHelpOptions = true, description = "CLI to manage Docker-based vulnerable labs")
public class CLI implements Callable<Integer> {
    
    @CommandLine.Option(names = {"-h", "--help"}, usageHelp = true, description = "Display help message")
    boolean helpRequested;

    @CommandLine.Option(names = {"-r","--run"}, description = "Start up vulnerable target")
    void start(@CommandLine.Parameters(paramLabel = "TARGET", description = "Vulnerable Target Name") String service) {
        DockerManager.startService(service);  // Uncomment this in production to actually start the service
    }

    @CommandLine.Option(names = {"-s","--search"}, description = "Search services")
    void search(@CommandLine.Parameters(paramLabel = "KEYWORD", description = "Keyword to search") String keyword) {
        ServiceManager.searchService(keyword);
    }

    @CommandLine.Command(name = "list", description = "List targets")
    int list() {
        System.out.println("Available vulnerable targets:");
        for (String service : ServiceManager.services.keySet()) {
            System.out.println(" # " + service);
        }
        return 0; // Success
    }

    @CommandLine.Option(names = {"-i","--inspect"}, description = "Inspect running service")
    void inspect(@CommandLine.Parameters(paramLabel = "SERVICE", description = "Service name") String service) {
        DockerManager.inspectService(service);
    }

    @Override
    public Integer call() throws Exception {
        System.err.println(" Use 'vt --help' for usage.");
        return 1;
    }
    
}
