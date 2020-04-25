#if (${PACKAGE_NAME} && ${PACKAGE_NAME} != "")package ${PACKAGE_NAME};#end
#if ($NAME.endsWith("Steps"))
    import org.fluentlenium.adapter.cucumber.FluentCucumberTest;
    /**
     * Step definitions for the .
     */
    public class ${NAME} extends FluentCucumberTest {

    }
#elseif($NAME.endsWith("Util") || $NAME.endsWith("Utils"))
    import lombok.AccessLevel;
    import lombok.NoArgsConstructor;
    /**
    * .
    */
    @NoArgsConstructor(access = AccessLevel.PRIVATE)
    public final class ${NAME} {

    }
#else
    #parse("File Header.java")
    public class ${NAME} {

    }
#end
