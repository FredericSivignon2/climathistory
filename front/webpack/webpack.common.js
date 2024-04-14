const path = require('path')
const HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = {
	entry: './src/index.tsx', // Point d'entrée modifié pour TypeScript
	output: {
		filename: '[name]-[contenthash].js',
		path: path.resolve(__dirname, 'dist'),
	},
	resolve: {
		// Ajouter des résolutions d'extension pour TypeScript
		extensions: ['.tsx', '.ts', '.js'],
	},
	module: {
		rules: [
			{
				test: /\.tsx?$/, // Règle pour les fichiers .ts et .tsx
				use: 'ts-loader',
				exclude: /node_modules/,
			},
			{ test: /\.css$/, use: ['style-loader', 'css-loader'] },
		],
	},
	// Ajoutez d'autres configurations ici si nécessaire
	plugins: [
		new HtmlWebpackPlugin({
			template: './public/index.html',
		}),
	],
}
