const path = require('path')

module.exports = {
	entry: './src/index.tsx', // Point d'entrée modifié pour TypeScript
	output: {
		filename: 'bundle.js',
		path: path.resolve(__dirname, 'dist'),
	},
	mode: 'development',
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
}
