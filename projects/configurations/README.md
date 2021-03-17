# Configurations (7)

  This section is all about configuration, from memory configuration to INI, JSON and XML.

  * [Configuration](/projects/configurations/configuration-1)

    This is the 'hello world' of configuration. Just use a memory based configuration and read/write values to/from it.

  * [Configuration - Options](/projects/configurations/configuration-options)

    Use IOptions at the most basic.

  * [Configuration - Environment variables](/projects/configurations/configuration-environment-variables)

    Load environment variables and display all of them.

  * [Configuration - INI file](/projects/configurations/configuration-ini)

    Read from INI file. 

  * [Configuration - INI file - Options](/projects/configurations/configuration-ini-options)

    Read from INI file (with nested keys) and IOptions. 

  * [Configuration - XML file](/projects/configurations/configuration-xml)

    Read from XML file. 

    **Note**: This Xml Configuration provider does not support repeated element.

    The following configuration settings will break:

    ```
    <appSettings>
      <add key="webpages:Version" value="3.0.0.0" />
      <add key="webpages:Enabled" value="false" />
    </appSettings>
    ```

    On the other hand you can get unlimited nested elements and also attributes.

  * [Configuration - XML file - Options](/projects/configurations/configuration-xml-options)

    Read from XML file and use IOptions. 

