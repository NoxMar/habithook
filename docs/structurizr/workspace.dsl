workspace {

    model {
        user = person "Użytkownik" "Użytkownik aplikacji do zarządzania nawykami"
        habitSystem = softwareSystem "HabitHook system" "System śledzący nawyki i ich status oraz pozwalający na użytkownikowi na interakcje z tymi danymi" {
            singlePageApp = container "Aplikacja typu SPA" "Zapewnia dostępną funckjonalność zarządzania nawykami i przeglądanie ich statusu przez przeglądarkę" "Blazor WASM" "Web Browser"
            apiBackend = container "Aplikacja API" "Udostępnia funkcję programatycznego zarządzania nawykami i sprawdzania ich stanu przez JSON+HTTPS" "ASP.NET Core"
            database = container "Baza danych" "Przechowuje dane związane z nawykami i ich integracjami" "T-SQL Schema" "Database"
        }
        externalWebhookSource = softwareSystem "Żródło webhook" "Zewnętrzne źródło danych o stanie nawyku zintegrowane przez Webhook" "Existing System"

        user -> habitSystem "Zarządza nawykami i sprawdza obecny stan ich wykonania, dodaje zewnętrzne źródła"
        externalWebhookSource -> habitSystem "Informuje o zdarzeniach związanych z nawykami" Webhook

        # relationships between containters and people
        user -> singlePageApp "Przegląda i modyfikuje dane używając"

        # relationships between containers and software systems
        externalWebhookSource -> apiBackend "Wysyła żądania o aktualizacji stanu związanymi z nawykami" "JSON/HTTPS"

        # relationshpis between containers and other containers
        singlePageApp -> apiBackend "Wysyła żądania do API" " JSON/HTTPS"
        apiBackend -> database "Odczytuje z / zapisuje do" "SQL/TCP"

        
        staticHosting = softwareSystem "Hosting statycznych pików" "Hostuje statyczne pliki będące elementami SPA" "Existing System"
        staticHosting -> singlePageApp "Dostarcza do przeglądarki" HTTPS
        deploymentEnvironment "Produkcja" {
            deploymentNode "Komputer użytwkonika" "" "Microsoft Windows, macOS, Linux, Andoird, lub iOS" {
                deploymentNode "Web " "" "Chrome, Firefox, Safari, inna przeglądarka bazująca na Chromium" {
                    spaProductionInstance = containerInstance singlePageApp
                }
            }

            deploymentNode "Chmura Azure" {
                deploymentNode "App Service Azure" {
                    apiBackentInstance = containerInstance apiBackend
                }

                deploymentNode "Azure SQL Database" {
                    databaseInstance = containerInstance database
                }

                deploymentNode "Azure Static Web App" {
                    staticHostingInstance = softwareSystemInstance staticHosting
                }
            }
        }
    }

    views {
        systemContext habitSystem "DiagramKontekstu" {
            include *
            exclude staticHosting
            autoLayout
        }
        container habitSystem "Kontenery" {
            include *
            exclude staticHosting
            autoLayout
        }
        deployment habitSystem "Produkcja" "Produkcja" {
            include *
            autoLayout
        }
        theme default
        styles {
            element "Existing System" {
                background #999999
                color #ffffff
            }
            element "Web Browser" {
                shape WebBrowser
            }
            element "Database" {
                shape Cylinder
            }
        }
    }
    
}