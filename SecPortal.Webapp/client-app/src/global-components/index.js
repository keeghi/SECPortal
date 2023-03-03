import Chart from './chart/Main.vue'
import Tippy from './tippy/Main.vue'
import TippyContent from './tippy-content/Main.vue'
import LoadingIcon from './loading-icon/Main.vue'
// import BalloonBlockEditor from './ckeditor/BalloonBlockEditor.vue'
// import BalloonEditor from './ckeditor/BalloonEditor.vue'
// import DocumentEditor from './ckeditor/DocumentEditor.vue'
// import InlineEditor from './ckeditor/InlineEditor.vue'
import * as featherIcons from '@zhuowenli/vue-feather-icons'

export default app => {
  app.component('Chart', Chart)
  app.component('Tippy', Tippy)
  app.component('TippyContent', TippyContent)
  app.component('LoadingIcon', LoadingIcon)
  // app.component('BalloonBlockEditor', BalloonBlockEditor)
  // app.component('BalloonEditor', BalloonEditor)
  // app.component('DocumentEditor', DocumentEditor)
  // app.component('InlineEditor', InlineEditor)

  for (const [key, icon] of Object.entries(featherIcons)) {
    icon.props.size.default = '24'
    app.component(key, icon)
  }
}
