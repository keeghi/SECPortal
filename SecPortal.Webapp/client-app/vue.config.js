const webpack = require('webpack')
const zlib = require('zlib')
const CompressionPlugin = require('compression-webpack-plugin')
// const BundleAnalyzerPlugin = require('webpack-bundle-analyzer')
//   .BundleAnalyzerPlugin

module.exports = {
  parallel: false,
  configureWebpack: {
    plugins: [
      new webpack.IgnorePlugin({
        resourceRegExp: /^\.\/locale$/,
        contextRegExp: /moment$/
      }),
      // new BundleAnalyzerPlugin(),
      new webpack.ProvidePlugin({
        cash: 'cash-dom',
        Popper: '@popperjs/core'
      }),
      new CompressionPlugin({
        filename: '[path][base].br',
        algorithm: 'brotliCompress',
        test: /\.js$/
      })
    ]
  },
  devServer: {
    onBeforeSetupMiddleware({ app }) {
      app.use('*.js', (req, res, next) => {
        if (req.get('Accept-Encoding')?.includes('br')) {
          req.url += '.br'
          res.set('Content-Encoding', 'br')
          res.set('Content-Type', 'application/javascript; charset=utf-8')
        }
        next()
      })
    }
  },
  pluginOptions: {
    compression: {
      brotli: {
        filename: '[file].br[query]',
        algorithm: 'brotliCompress',
        include: /\.(js|css|html|svg|json)(\?.*)?$/i,
        compressionOptions: {
          params: {
            [zlib.constants.BROTLI_PARAM_QUALITY]: 11
          }
        },
        minRatio: 0.8
      },
      gzip: {
        filename: '[file].gz[query]',
        algorithm: 'gzip',
        include: /\.(js|css|html|svg|json)(\?.*)?$/i,
        minRatio: 0.8
      }
    }
  }
}
