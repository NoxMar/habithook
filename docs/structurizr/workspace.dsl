workspace {

    model {
        user = person "User"
        habitSystem = softwareSystem "HabitHook system" "System śledzący nawyki i ich status oraz pozwalający na użytkownikowi na interakcje z tymi danymi"
        externalWebhookSource = softwareSystem "Żródło webhook" "Zewnętrzne źródło danych o stanie nawyku zintegrowane przez Webhook" "Existing System"
        

        user -> habitSystem "Uses"
        habitSystem -> externalWebhookSource
    }

    views {
        systemContext habitSystem "DiagramKontekstu" {
            include *
            autoLayout
        }
        theme default
        styles {
            element "Existing System" {
                background #999999
                color #ffffff
            }
        }
    }
    
}