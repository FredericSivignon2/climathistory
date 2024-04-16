const { merge } = require('webpack-merge')
const common = require('./webpack.common.js')
const TerserPlugin = require('terser-webpack-plugin')
const CompressionPlugin = require('compression-webpack-plugin')
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin')
const Dotenv = require('dotenv-webpack')

module.exports = merge(common, {
	mode: 'production',
	devtool: 'source-map',
	plugins: [
		new CompressionPlugin({
			test: /\.js(\?.*)?$/i,
		}),
		new Dotenv({
			path: './.env',
		}),
	],
	optimization: {
		minimize: true,
		minimizer: [new TerserPlugin(), new CssMinimizerPlugin()],
		splitChunks: {
			chunks: 'all',
		},
	},
})
