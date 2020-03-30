import * as b from "bobril";
import * as monaco from "monaco-editor";
import { MonacoComponent } from "../../monaco/index";
import { cover100, fullyCoveredStyle, partiallyCoveredStyle, notCoveredStyle, clickable } from "../../styles";
import * as model from "../../model/index";
import { goToUp } from "../../model/routeTransitions";

monaco.languages.typescript.typescriptDefaults.setDiagnosticsOptions({ noSemanticValidation: true });

export function FilePage(data: { name: string }): b.IBobrilNode {
    var store = b.useContext(model.CoverageContext);
    var details = store.json[data.name];
    if (details == undefined) {
        b.runTransition(b.createRedirectReplace("rootdir"));
        return undefined;
    }
    let lang: string;
    if (/\.tsx?$/i.test(data.name)) lang = "typescript";
    else if (/\.json$/i.test(data.name)) lang = "json";
    else lang = "javascript";

    function init(editor: monaco.editor.IStandaloneCodeEditor) {
        editor.updateOptions({ readOnly: true });
        editor.setValue(details.source);
        let r = details.encodedRanges;
        let decorations: monaco.editor.IModelDeltaDecoration[] = [];
        for (let i = 0; i < r.length; ) {
            var range = new monaco.Range(r[i + 1] + 1, r[i + 2] + 1, r[i + 3] + 1, r[i + 4] + 1);
            var hover = "";
            switch (r[i]) {
                case 0:
                    hover = "Statement: " + r[i + 5];
                    break;
                case 1:
                    hover = "Condition Falsy: " + r[i + 5] + " Truthy: " + r[i + 6];
                    break;
                case 2:
                    hover = "Function: " + r[i + 5];
                    break;
                case 3:
                    hover = "Switch Branch: " + r[i + 5];
                    break;
            }
            let covType = 0;
            if (r[i] != 1) {
                if (r[i + 5]) covType = 2;
            } else {
                if (r[i + 5]) covType++;
                if (r[i + 6]) covType++;
            }
            decorations.push({
                range,
                options: {
                    inlineClassName:
                        covType === 2 ? fullyCoveredStyle : covType === 1 ? partiallyCoveredStyle : notCoveredStyle,
                    hoverMessage: { value: hover }
                }
            });
            i += r[i] != 1 ? 6 : 7;
        }

        editor.deltaDecorations([], decorations);
    }

    return (
        <>
            <div style={{ width: "100%" }}>
                <span onClick={goToUp(data.name)} style={[clickable, { fontStyle: "italic", padding: 1 }]}>
                    (up)
                </span>{" "}
                <span>{data.name}</span>
            </div>
            <MonacoComponent language={lang} style={cover100} onInit={init} />
        </>
    );
}
