const webpack = require('webpack')

module.exports = {
    entry: './src/index.js',
    output: {
        path: './dist',
        filename: 'index.js'
    },
    resolve: {
        extensions: ['.js', '.vue', '.json']
    },
    module: {
        loaders: [{
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                include: __dirname,
                exclude: /node_modules/
            },
            {
                test: /\.s[a|c]ss$/,
                loader: 'style!css!sass'
            }
        ]
    },
    vue: {
        loaders: {
            scss: 'style!css!sass'
        }
    },
    plugins: [
        new webpack.optimize.UglifyJsPlugin({
            minimize: true,
            sourceMap: false,
            mangle: true,
            compress: {
                warnings: false
            }
        })
    ]
}